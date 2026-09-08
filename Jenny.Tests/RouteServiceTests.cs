using Jenny.Core.Services;
using Jenny.Data.Repositories;
using Jenny.Data.Seed;

namespace Jenny.Tests;

public sealed class RouteServiceTests
{
    [Fact]
    public async Task GetRoutesAsync_ReturnsSupportedTransportOnly()
    {
        var store = MockTravelDataStore.CreateSeeded();
        var bangkok = store.Locations.Single(location => location.Name == "Bangkok");
        var singapore = store.Locations.Single(location => location.Name == "Singapore");
        var service = new RouteService(new RouteRepository(store), new TransportationService());

        var routes = await service.GetRoutesAsync(bangkok.Id, singapore.Id);

        Assert.NotEmpty(routes);
        Assert.All(routes, route => Assert.Contains(route.TransportType.ToString(), new[] { "Train", "Ferry" }));
        Assert.Equal("Train", routes.First().TransportType.ToString());
    }
}
