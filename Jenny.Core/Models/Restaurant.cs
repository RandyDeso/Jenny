using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents a dining recommendation.
/// </summary>
public sealed class Restaurant
{
    /// <summary>
    /// Gets or sets the restaurant identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the restaurant name.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cuisine style.
    /// </summary>
    [Required]
    public string Cuisine { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the related location.
    /// </summary>
    [Required]
    public Location Location { get; set; } = new();

    /// <summary>
    /// Gets or sets the average rating.
    /// </summary>
    [Range(0, 5)]
    public double Rating { get; set; }

    /// <summary>
    /// Gets or sets the price range label.
    /// </summary>
    [Required]
    public string PriceRange { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [Required]
    public string Description { get; set; } = string.Empty;
}
