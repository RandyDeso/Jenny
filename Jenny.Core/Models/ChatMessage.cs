using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents a single chat message.
/// </summary>
public sealed class ChatMessage
{
    /// <summary>
    /// Gets or sets the sender.
    /// </summary>
    public MessageSender Sender { get; set; }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets or sets the message type.
    /// </summary>
    public ChatMessageType Type { get; set; } = ChatMessageType.Text;
}
