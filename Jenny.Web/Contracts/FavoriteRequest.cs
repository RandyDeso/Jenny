using System.ComponentModel.DataAnnotations;

namespace Jenny.Web.Contracts;

/// <summary>
/// Represents a favorite creation payload.
/// </summary>
public sealed class FavoriteRequest
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the favorite type.
    /// </summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the related entity identifier.
    /// </summary>
    [Required]
    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the display label.
    /// </summary>
    [Required]
    public string Label { get; set; } = string.Empty;
}
