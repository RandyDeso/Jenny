using Jenny.Core.Services;
using Jenny.Data.Repositories;
using Jenny.Data.Seed;

namespace Jenny.Tests;

public sealed class ChatServiceTests
{
    [Fact]
    public async Task ProcessMessageAsync_ReturnsActivityRecommendations_AndPersistsHistory()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var locationService = new LocationService(new LocationRepository(store));
        var activityService = new ActivityService(new ActivityRepository(store));
        var restaurantService = new RestaurantService(new RestaurantRepository(store));
        var routeService = new RouteService(new RouteRepository(store), new TransportationService());
        var userService = new UserService(new UserRepository(store));
        var chatService = new ChatService(
            new ConversationRepository(store),
            locationService,
            activityService,
            routeService,
            restaurantService,
            new TravelTipRepository(store),
            userService);
        var userId = Guid.NewGuid();

        var response = await chatService.ProcessMessageAsync(userId, "What activities should I do in Hong Kong?");
        var history = await chatService.GetHistoryAsync(userId);

        Assert.Equal("activities", response.Intent);
        Assert.Contains("Victoria Peak Tram", response.Reply);
        Assert.Equal(2, history.Messages.Count);
    }
}
