using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents an application user.
/// </summary>
public sealed class User
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user email address.
    /// </summary>
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets saved travel preferences.
    /// </summary>
    [Required]
    public TravelPreferences Preferences { get; set; } = new();

    /// <summary>
    /// Gets or sets saved favorites.
    /// </summary>
    public IList<UserFavorite> Favorites { get; set; } = new List<UserFavorite>();
}
