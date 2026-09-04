namespace Jenny.Core.Models;

/// <summary>
/// Supported activity categories.
/// </summary>
public enum ActivityCategory
{
    Sightseeing,
    Food,
    Culture,
    Nature,
    Nightlife
}

/// <summary>
/// Supported transportation types.
/// </summary>
public enum TransportType
{
    Train,
    Ferry
}

/// <summary>
/// Indicates the complexity of a route.
/// </summary>
public enum RouteDifficulty
{
    Easy,
    Moderate,
    Complex
}

/// <summary>
/// Indicates the sender for a chat message.
/// </summary>
public enum MessageSender
{
    User,
    Assistant
}

/// <summary>
/// Indicates the semantic type of a chat message.
/// </summary>
public enum ChatMessageType
{
    Text,
    Recommendation,
    Clarification,
    Error
}

/// <summary>
/// Indicates the preferred travel pace.
/// </summary>
public enum TravelPace
{
    Relaxed,
    Balanced,
    Fast
}
