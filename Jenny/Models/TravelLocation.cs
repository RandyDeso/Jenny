namespace Jenny.Models;

public sealed record TravelLocation(
    string Name,
    string Country,
    string Summary,
    string Weather,
    IReadOnlyList<string> Activities,
    IReadOnlyList<string> Restaurants,
    IReadOnlyList<string> Accommodations,
    IReadOnlyList<string> Tips);
