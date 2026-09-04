using Jenny.Data;
using Jenny.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace Jenny.Tests;

public sealed class TravelAssistantServiceTests
{
    private readonly TravelAssistantService service = new(new TravelData(), NullLogger<TravelAssistantService>.Instance);

    [Fact]
    public void Greeting_ExplainsTrainAndFerryFocus()
    {
        var response = service.GetResponse("hello");

        Assert.Equal("greeting", response.Intent);
        Assert.Contains("train-and-ferry", response.Reply, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void RouteQuestion_ReturnsPlaneFreeMultiLegJourney()
    {
        var response = service.GetResponse("How do I get from London to Dublin without flying?");

        var option = Assert.Single(response.Options);
        Assert.Equal("route", response.Intent);
        Assert.Contains("avoids flights", response.Reply, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("7h 15m", option.TravelTime);
        Assert.Contains(option.Details, detail => detail.Contains("ferry", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void RestaurantQuestion_ReturnsDestinationDiningSuggestions()
    {
        var response = service.GetResponse("Where should I eat in Amsterdam?");

        var option = Assert.Single(response.Options);
        Assert.Equal("restaurants", response.Intent);
        Assert.Contains("Amsterdam", option.Title);
        Assert.Contains(option.Details, detail => detail.Contains("Foodhallen", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void RouteQuestionWithoutBothLocations_AsksForClarification()
    {
        var response = service.GetResponse("Can you plan a train route?");

        Assert.Equal("route", response.Intent);
        Assert.Empty(response.Options);
        Assert.Contains("starting point and destination", response.Reply, StringComparison.OrdinalIgnoreCase);
    }
}
