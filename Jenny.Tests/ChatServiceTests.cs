using Jenny.Core.Services;
using Jenny.Data.Infrastructure;
using Jenny.Data.Repositories;
using Jenny.Data.Seed;

namespace Jenny.Tests;

public sealed class ChatServiceTests
{
    private static ChatService CreateService(MockTravelDataStore store)
    {
        var locationService = new LocationService(new LocationRepository(store));
        var activityService = new ActivityService(new ActivityRepository(store));
        var restaurantService = new RestaurantService(new RestaurantRepository(store));
        var routeService = new RouteService(new RouteRepository(store), new TransportationService());
        var keyedLock = new KeyedStateLock();
        var userService = new UserService(new UserRepository(store), new UserStateLock(keyedLock));

        return new ChatService(
            new ConversationRepository(store),
            locationService,
            activityService,
            routeService,
            restaurantService,
            new TravelTipRepository(store),
            userService,
            new ConversationStateLock(keyedLock));
    }

    [Fact]
    public async Task ProcessMessageAsync_ReturnsActivityRecommendations_AndPersistsHistory()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var chatService = CreateService(store);
        var userId = Guid.NewGuid();

        var response = await chatService.ProcessMessageAsync(userId, "What activities should I do in Hong Kong?");
        var history = await chatService.GetHistoryAsync(userId);

        Assert.Equal("activities", response.Intent);
        Assert.Contains("Victoria Peak Tram", response.Reply);
        Assert.Equal(2, history.Messages.Count);
    }

    [Fact]
    public async Task ProcessMessageAsync_PreservesRouteDirectionFromMessageText()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var chatService = CreateService(store);

        var response = await chatService.ProcessMessageAsync(Guid.NewGuid(), "How do I get from Singapore to Bangkok?");

        Assert.Equal("routes", response.Intent);
        Assert.False(response.RequiresClarification);
        Assert.Contains("from Singapore to Bangkok", response.Reply);
    }

    [Fact]
    public async Task ProcessMessageAsync_AsksForLocation_WhenRestaurantRequestHasNoContext()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var chatService = CreateService(store);

        var response = await chatService.ProcessMessageAsync(Guid.NewGuid(), "Where should I eat?");

        Assert.Equal("restaurants", response.Intent);
        Assert.True(response.RequiresClarification);
        Assert.Contains("Which location should I search", response.Reply);
    }

    [Fact]
    public async Task ProcessMessageAsync_UsesStoredLocationContext_ForRestaurantFollowUp()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var chatService = CreateService(store);
        var userId = Guid.NewGuid();

        await chatService.ProcessMessageAsync(userId, "Tell me about Hong Kong");
        var response = await chatService.ProcessMessageAsync(userId, "Where should I eat?");

        Assert.Equal("restaurants", response.Intent);
        Assert.False(response.RequiresClarification);
        Assert.Contains("Dining ideas in Hong Kong", response.Reply);
    }

    [Fact]
    public async Task ProcessMessageAsync_UsesStoredLocationContext_ForRouteFollowUp()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var chatService = CreateService(store);
        var userId = Guid.NewGuid();

        await chatService.ProcessMessageAsync(userId, "Tell me about Singapore");
        var response = await chatService.ProcessMessageAsync(userId, "How do I get to Bangkok?");

        Assert.Equal("routes", response.Intent);
        Assert.False(response.RequiresClarification);
        Assert.Contains("from Singapore to Bangkok", response.Reply);
    }
}
