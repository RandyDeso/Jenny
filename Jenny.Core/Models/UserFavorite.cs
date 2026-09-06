using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents a saved favorite item.
/// </summary>
public sealed class UserFavorite
{
    /// <summary>
    /// Gets or sets the favorite identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the favorite type.
    /// </summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the referenced entity identifier.
    /// </summary>
    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the display label.
    /// </summary>
    [Required]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the favorite was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
