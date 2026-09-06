using System.Text;
using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Implements the travel chat engine.
/// </summary>
public sealed class ChatService(
    IConversationRepository conversationRepository,
    ILocationService locationService,
    IActivityService activityService,
    IRouteService routeService,
    IRestaurantService restaurantService,
    ITravelTipRepository travelTipRepository,
    IUserService userService,
    IConversationStateLock conversationStateLock) : IChatService
{
    /// <inheritdoc />
    public async Task<Conversation> GetHistoryAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await userService.GetOrCreateAsync(userId, cancellationToken: cancellationToken);
        return await conversationStateLock.ExecuteAsync(userId, async () =>
            await conversationRepository.GetByUserIdAsync(userId, cancellationToken) ?? new Conversation { UserId = userId });
    }

    /// <inheritdoc />
    public async Task<ChatResponse> ProcessMessageAsync(Guid userId, string message, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        await userService.GetOrCreateAsync(userId, cancellationToken: cancellationToken);
        return await conversationStateLock.ExecuteAsync(userId, async () =>
        {
            var conversation = await conversationRepository.GetByUserIdAsync(userId, cancellationToken) ?? new Conversation { UserId = userId };
            conversation.Messages.Add(new ChatMessage
            {
                Sender = MessageSender.User,
                Content = message,
                Type = ChatMessageType.Text,
                Timestamp = DateTimeOffset.UtcNow
            });

            var matchedLocations = (await FindLocationsAsync(message, cancellationToken)).ToArray();
            var intent = DetectIntent(message);
            var reply = await BuildReplyAsync(intent, message, matchedLocations, conversation, cancellationToken);
            if (matchedLocations.Length > 0)
            {
                conversation.Context["lastLocationId"] = matchedLocations[0].Id.ToString();
                conversation.Context["lastLocationName"] = matchedLocations[0].Name;
            }

            conversation.Context["lastIntent"] = intent;
            conversation.Messages.Add(new ChatMessage
            {
                Sender = MessageSender.Assistant,
                Content = reply.Reply,
                Type = reply.RequiresClarification ? ChatMessageType.Clarification : ChatMessageType.Recommendation,
                Timestamp = DateTimeOffset.UtcNow
            });

            await conversationRepository.SaveAsync(conversation, cancellationToken);
            reply.Conversation = conversation;
            return reply;
        });
    }

    private static string DetectIntent(string message)
    {
        var normalized = message.ToLowerInvariant();
        if (normalized.Contains("route")
            || normalized.Contains("travel")
            || normalized.Contains("get from")
            || normalized.Contains("get to")
            || normalized.Contains("to ")
            || normalized.Contains("from "))
        {
            return "routes";
        }

        if (normalized.Contains("restaurant") || normalized.Contains("food") || normalized.Contains("eat") || normalized.Contains("dining"))
        {
            return "restaurants";
        }

        if (normalized.Contains("activity") || normalized.Contains("things to do") || normalized.Contains("attraction") || normalized.Contains("do in"))
        {
            return "activities";
        }

        if (normalized.Contains("tip") || normalized.Contains("advice") || normalized.Contains("recommendation"))
        {
            return "tips";
        }

        return "general";
    }

    private async Task<IReadOnlyCollection<Location>> FindLocationsAsync(string message, CancellationToken cancellationToken)
    {
        var allLocations = await locationService.GetAllAsync(cancellationToken);
        return allLocations
            .Select(location => new
            {
                Location = location,
                Position = message.IndexOf(location.Name, StringComparison.OrdinalIgnoreCase)
            })
            .Where(match => match.Position >= 0)
            .OrderBy(match => match.Position)
            .ThenBy(match => match.Location.Name)
            .Select(match => match.Location)
            .ToArray();
    }

    private async Task<ChatResponse> BuildReplyAsync(string intent, string message, IReadOnlyCollection<Location> matchedLocations, Conversation conversation, CancellationToken cancellationToken)
    {
        return intent switch
        {
            "activities" => await BuildActivityReplyAsync(matchedLocations, conversation, cancellationToken),
            "restaurants" => await BuildRestaurantReplyAsync(matchedLocations, conversation, cancellationToken),
            "routes" => await BuildRouteReplyAsync(message, matchedLocations, conversation, cancellationToken),
            "tips" => await BuildTipsReplyAsync(matchedLocations, cancellationToken),
            _ => await BuildGeneralReplyAsync(message, matchedLocations, cancellationToken)
        };
    }

    private async Task<ChatResponse> BuildActivityReplyAsync(IReadOnlyCollection<Location> matchedLocations, Conversation conversation, CancellationToken cancellationToken)
    {
        var location = await ResolveLocationAsync(matchedLocations, conversation, cancellationToken);
        if (location is null)
        {
            return Clarification("activities", "Tell me which city or terminal you're interested in, such as Hong Kong, Macau, Bangkok, or Singapore, and I can suggest activities.");
        }

        var activities = await activityService.GetByLocationAsync(location.Id, cancellationToken);
        if (activities.Count == 0)
        {
            return Clarification("activities", $"I don't have activities for {location.Name} yet. Try Hong Kong, Macau, Bangkok, Singapore, Ayutthaya, or Phuket.");
        }

        var builder = new StringBuilder($"Top activities in {location.Name}: ");
        builder.AppendJoin("; ", activities.Select(activity => $"{activity.Name} ({activity.Category}, {activity.Duration.TotalHours:0.#}h, ${activity.Cost:0})"));
        return Success("activities", builder.ToString());
    }

    private async Task<ChatResponse> BuildRestaurantReplyAsync(IReadOnlyCollection<Location> matchedLocations, Conversation conversation, CancellationToken cancellationToken)
    {
        var location = await ResolveLocationAsync(matchedLocations, conversation, cancellationToken);
        if (location is null)
        {
            return Clarification("restaurants", "Which location should I search for restaurants? You can ask about Hong Kong, Macau, Bangkok, Singapore, Ayutthaya, or Phuket.");
        }

        var restaurants = await restaurantService.GetByLocationAsync(location.Id, cancellationToken);
        if (restaurants.Count == 0)
        {
            return Clarification("restaurants", $"I don't have restaurant picks for {location.Name} yet. Try another stop like Hong Kong, Bangkok, or Singapore.");
        }

        var builder = new StringBuilder($"Dining ideas in {location.Name}: ");
        builder.AppendJoin("; ", restaurants.Select(restaurant => $"{restaurant.Name} ({restaurant.Cuisine}, {restaurant.PriceRange}, {restaurant.Rating:0.0}/5)"));
        return Success("restaurants", builder.ToString());
    }

    private async Task<ChatResponse> BuildRouteReplyAsync(string message, IReadOnlyCollection<Location> matchedLocations, Conversation conversation, CancellationToken cancellationToken)
    {
        var endpoints = await ResolveRouteEndpointsAsync(message, matchedLocations, conversation, cancellationToken);
        if (endpoints is null)
        {
            return Clarification("routes", "Please mention both your origin and destination so I can suggest train or ferry routes.");
        }

        var (origin, destination) = endpoints.Value;
        var routes = await routeService.GetRoutesAsync(origin.Id, destination.Id, cancellationToken);
        if (routes.Count == 0)
        {
            return Clarification("routes", $"I couldn't find a train or ferry route from {origin.Name} to {destination.Name} yet.");
        }

        var builder = new StringBuilder($"Best routes from {origin.Name} to {destination.Name}: ");
        builder.AppendJoin("; ", routes.Select(route => $"{route.TransportType} via {string.Join(", ", route.Stops)} in {route.Duration.TotalHours:0.#}h for about ${route.Cost:0}"));
        return Success("routes", builder.ToString());
    }

    private async Task<ChatResponse> BuildTipsReplyAsync(IReadOnlyCollection<Location> matchedLocations, CancellationToken cancellationToken)
    {
        var locationId = matchedLocations.FirstOrDefault()?.Id;
        var tips = await travelTipRepository.GetTipsAsync(locationId, cancellationToken);
        return Success("tips", $"Travel tips: {string.Join("; ", tips)}");
    }

    private async Task<ChatResponse> BuildGeneralReplyAsync(string message, IReadOnlyCollection<Location> matchedLocations, CancellationToken cancellationToken)
    {
        var location = matchedLocations.FirstOrDefault();
        if (location is not null)
        {
            var tips = await travelTipRepository.GetTipsAsync(location.Id, cancellationToken);
            return Success("general", $"I can help with activities, restaurants, routes, and travel tips for {location.Name}. Here's a quick tip: {tips.FirstOrDefault() ?? "Ask me what to do there."}");
        }

        return Success("general", "I can help with train and ferry routes, activities, restaurants, and local tips. Ask about places like Hong Kong, Macau, Bangkok, Singapore, Ayutthaya, or Phuket.");
    }

    private async Task<Location?> ResolveLocationAsync(IReadOnlyCollection<Location> matchedLocations, Conversation conversation, CancellationToken cancellationToken)
    {
        var directMatch = matchedLocations.FirstOrDefault();
        if (directMatch is not null)
        {
            return directMatch;
        }

        if (conversation.Context.TryGetValue("lastLocationId", out var lastLocationId) && Guid.TryParse(lastLocationId, out var parsedLocationId))
        {
            return await locationService.GetByIdAsync(parsedLocationId, cancellationToken);
        }

        return null;
    }

    private async Task<(Location Origin, Location Destination)?> ResolveRouteEndpointsAsync(string message, IReadOnlyCollection<Location> matchedLocations, Conversation conversation, CancellationToken cancellationToken)
    {
        var locations = matchedLocations.ToArray();
        if (locations.Length >= 2)
        {
            return (locations[0], locations[1]);
        }

        if (locations.Length != 1)
        {
            return null;
        }

        var contextualLocation = await ResolveLocationAsync(Array.Empty<Location>(), conversation, cancellationToken);
        if (contextualLocation is null || contextualLocation.Id == locations[0].Id)
        {
            return null;
        }

        var normalized = message.ToLowerInvariant();
        return normalized.Contains("from ", StringComparison.Ordinal)
            ? (locations[0], contextualLocation)
            : (contextualLocation, locations[0]);
    }

    private static ChatResponse Success(string intent, string reply) => new()
    {
        Intent = intent,
        Reply = reply,
        RequiresClarification = false
    };

    private static ChatResponse Clarification(string intent, string reply) => new()
    {
        Intent = intent,
        Reply = reply,
        RequiresClarification = true,
        Conversation = new Conversation()
    };
}
