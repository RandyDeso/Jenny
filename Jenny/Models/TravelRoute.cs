namespace Jenny.Models;

public sealed record TravelRoute(
    string Origin,
    string Destination,
    string Summary,
    string TravelTime,
    string CostEstimate,
    IReadOnlyList<string> Legs);
