using Jenny.Contracts;
using Jenny.Data;
using Jenny.Models;

namespace Jenny.Services;

public sealed class TravelAssistantService(
    TravelData travelData,
    ILogger<TravelAssistantService> logger) : ITravelAssistantService
{
    public ChatResponse GetResponse(string message)
    {
        var normalizedMessage = message.Trim();
        var loweredMessage = normalizedMessage.ToLowerInvariant();
        var locations = MatchLocations(loweredMessage);

        logger.LogInformation("Handling travel question. Locations detected: {Locations}", string.Join(", ", locations.Select(location => location.Name)));

        if (HasAnyKeyword(loweredMessage, "plane", "planes", "flight", "flights", "airfare", "airport"))
        {
            return new ChatResponse(
                "I’ll keep this plane-free and focus on trains and ferries instead. Tell me your route or destination and I’ll suggest the best ground or water-based option.",
                "travel-preference",
                [],
                ["Plan a route from London to Dublin", "Things to do in Paris", "Best food in Amsterdam"]);
        }

        if (IsRouteIntent(loweredMessage))
        {
            return BuildRouteResponse(locations);
        }

        if (IsRestaurantIntent(loweredMessage))
        {
            return BuildLocationResponse(
                locations,
                "restaurants",
                location => location.Restaurants,
                location => $"{location.Name} is great for relaxed dining between rail or ferry legs. Here are a few places worth booking or dropping into.");
        }

        if (IsAccommodationIntent(loweredMessage))
        {
            return BuildLocationResponse(
                locations,
                "accommodation",
                location => location.Accommodations,
                location => $"For {location.Name}, I’d stay somewhere with easy station or port access so your train-and-ferry plans stay simple.");
        }

        if (IsWeatherIntent(loweredMessage))
        {
            return BuildLocationResponse(
                locations,
                "weather",
                location => [location.Weather],
                location => $"Here’s the quick weather planning note for {location.Name}.");
        }

        if (IsActivityIntent(loweredMessage) || locations.Count > 0)
        {
            return BuildDiscoveryResponse(locations);
        }

        return new ChatResponse(
            "Hi, I’m Jenny — your train-and-ferry travel assistant. Ask me for routes, activities, restaurants, accommodation ideas, weather notes, or local tips for places like London, Paris, Amsterdam, Dublin, and Edinburgh.",
            "greeting",
            [],
            ["Plan a route from London to Dublin", "Things to do in Paris", "Where should I eat in Amsterdam?"]);
    }

    private ChatResponse BuildRouteResponse(IReadOnlyList<TravelLocation> locations)
    {
        if (locations.Count < 2)
        {
            return new ChatResponse(
                "I can plan a train or ferry route for you — just tell me both the starting point and destination, like “London to Dublin” or “Paris to Amsterdam.”",
                "route",
                [],
                ["London to Dublin", "Paris to Amsterdam", "Amsterdam to London"]);
        }

        var route = travelData.Routes.FirstOrDefault(candidate =>
            candidate.Origin.Equals(locations[0].Name, StringComparison.OrdinalIgnoreCase) &&
            candidate.Destination.Equals(locations[1].Name, StringComparison.OrdinalIgnoreCase));

        if (route is null)
        {
            return new ChatResponse(
                $"I don’t have a saved sample route from {locations[0].Name} to {locations[1].Name} yet, but I can still help with activities, food, or accommodation in either city.",
                "route",
                [],
                ["Things to do there", "Find restaurants", "Suggest hotels"]);
        }

        return new ChatResponse(
            $"{route.Origin} to {route.Destination} works well by {DescribeRouteMode(route)}. This sample plan avoids flights and keeps the journey station-to-station or port-to-port.",
            "route",
            [new TravelOption($"{route.Origin} → {route.Destination}", route.Summary, route.Legs, route.TravelTime, route.CostEstimate)],
            ["Add destination activities", $"Restaurants in {route.Destination}", $"Where to stay in {route.Destination}"]);
    }

    private ChatResponse BuildDiscoveryResponse(IReadOnlyList<TravelLocation> locations)
    {
        if (locations.Count == 0)
        {
            return new ChatResponse(
                "Tell me the destination you have in mind and I’ll suggest activities, restaurants, local tips, and train-or-ferry-friendly travel ideas.",
                "discovery",
                [],
                ["Paris ideas", "Dublin food", "London route ideas"]);
        }

        var location = locations[0];
        var options = new List<TravelOption>
        {
            new("Top activities", $"Best things to do in {location.Name}.", location.Activities),
            new("Local food picks", $"Useful dining options in {location.Name}.", location.Restaurants),
            new("Travel-smart tips", $"Handy notes for a smoother trip in {location.Name}.", location.Tips)
        };

        return new ChatResponse(
            $"{location.Name} is a strong pick for a rail-first trip. Here’s a quick mix of sightseeing, food, and practical planning notes.",
            "discovery",
            options,
            [$"Plan a route to {location.Name}", $"Where should I stay in {location.Name}?", $"What’s the weather like in {location.Name}?"]);
    }

    private ChatResponse BuildLocationResponse(
        IReadOnlyList<TravelLocation> locations,
        string intent,
        Func<TravelLocation, IReadOnlyList<string>> selector,
        Func<TravelLocation, string> replyFactory)
    {
        if (locations.Count == 0)
        {
            return new ChatResponse(
                $"Happy to help with {intent}. Which destination are you thinking about?",
                intent,
                [],
                ["Paris", "Amsterdam", "Dublin"]);
        }

        var location = locations[0];
        return new ChatResponse(
            replyFactory(location),
            intent,
            [new TravelOption(location.Name, location.Summary, selector(location))],
            [$"Things to do in {location.Name}", $"Plan a route to {location.Name}", $"Local tips for {location.Name}"]);
    }

    private List<TravelLocation> MatchLocations(string loweredMessage) =>
        travelData.Locations
            .Where(location => loweredMessage.Contains(location.Name.ToLowerInvariant(), StringComparison.Ordinal))
            .ToList();

    private static bool IsRouteIntent(string loweredMessage) =>
        HasAnyKeyword(loweredMessage, "route", "travel", "journey", "get to", "go to", "from", "connection", "connections", "train", "ferry");

    private static bool IsRestaurantIntent(string loweredMessage) =>
        HasAnyKeyword(loweredMessage, "restaurant", "restaurants", "food", "eat", "dining", "dinner", "lunch", "breakfast");

    private static bool IsAccommodationIntent(string loweredMessage) =>
        HasAnyKeyword(loweredMessage, "hotel", "stay", "accommodation", "hostel", "sleep", "lodging");

    private static bool IsWeatherIntent(string loweredMessage) =>
        HasAnyKeyword(loweredMessage, "weather", "temperature", "rain", "forecast");

    private static bool IsActivityIntent(string loweredMessage) =>
        HasAnyKeyword(loweredMessage, "activity", "activities", "things to do", "attractions", "see", "visit");

    private static string DescribeRouteMode(TravelRoute route) =>
        route.Legs.Any(leg => leg.Contains("ferry", StringComparison.OrdinalIgnoreCase))
            ? "train and ferry"
            : "train";

    private static bool HasAnyKeyword(string input, params string[] keywords) =>
        keywords.Any(keyword => input.Contains(keyword, StringComparison.OrdinalIgnoreCase));
}
