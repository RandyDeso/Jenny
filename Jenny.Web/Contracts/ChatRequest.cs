using System.ComponentModel.DataAnnotations;

namespace Jenny.Web.Contracts;

/// <summary>
/// Represents a chat request payload.
/// </summary>
public sealed class ChatRequest
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the message text.
    /// </summary>
    [Required]
    public string Message { get; set; } = string.Empty;
}
