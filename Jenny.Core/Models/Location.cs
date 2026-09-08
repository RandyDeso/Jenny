using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents a travel destination or transport hub.
/// </summary>
public sealed class Location
{
    /// <summary>
    /// Gets or sets the location identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the map coordinates.
    /// </summary>
    [Required]
    public Coordinates Coordinates { get; set; } = new();

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    [Required]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the region.
    /// </summary>
    [Required]
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [Required]
    public string Description { get; set; } = string.Empty;
}
