namespace Jenny.Core.Models;

/// <summary>
/// Represents a conversation for a user.
/// </summary>
public sealed class Conversation
{
    /// <summary>
    /// Gets or sets the conversation identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the chat messages.
    /// </summary>
    public IList<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    /// <summary>
    /// Gets or sets arbitrary conversation context.
    /// </summary>
    public IDictionary<string, string> Context { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
}
