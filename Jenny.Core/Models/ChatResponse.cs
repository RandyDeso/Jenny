namespace Jenny.Core.Models;

/// <summary>
/// Represents a chat engine response.
/// </summary>
public sealed class ChatResponse
{
    /// <summary>
    /// Gets or sets the detected intent.
    /// </summary>
    public string Intent { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the assistant reply.
    /// </summary>
    public string Reply { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the user should clarify the request.
    /// </summary>
    public bool RequiresClarification { get; set; }

    /// <summary>
    /// Gets or sets the updated conversation.
    /// </summary>
    public Conversation Conversation { get; set; } = new();
}
