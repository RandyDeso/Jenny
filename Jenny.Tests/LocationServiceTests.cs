using Jenny.Core.Services;
using Jenny.Data.Repositories;
using Jenny.Data.Seed;

namespace Jenny.Tests;

public sealed class LocationServiceTests
{
    [Fact]
    public async Task SearchAsync_ReturnsMatchingLocation()
    {
        var service = new LocationService(new LocationRepository(MockTravelDataStore.CreateSeeded()));

        var results = await service.SearchAsync("kong");

        Assert.Contains(results, location => location.Name == "Hong Kong");
    }

    [Fact]
    public async Task SearchAsync_PrioritizesExactNameMatches()
    {
        var service = new LocationService(new LocationRepository(MockTravelDataStore.CreateSeeded()));

        var results = await service.SearchAsync("Bangkok");

        Assert.Equal("Bangkok", results.First().Name);
    }
}
