namespace Jenny.Contracts;

public sealed record ChatResponse(
    string Reply,
    string Intent,
    IReadOnlyList<TravelOption> Options,
    IReadOnlyList<string> QuickReplies);

public sealed record TravelOption(
    string Title,
    string Summary,
    IReadOnlyList<string> Details,
    string? TravelTime = null,
    string? CostEstimate = null);
