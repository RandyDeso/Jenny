using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents a destination activity recommendation.
/// </summary>
public sealed class Activity
{
    /// <summary>
    /// Gets or sets the activity identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the activity name.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity description.
    /// </summary>
    [Required]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity category.
    /// </summary>
    public ActivityCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the related location.
    /// </summary>
    [Required]
    public Location Location { get; set; } = new();

    /// <summary>
    /// Gets or sets the typical duration.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets or sets the indicative cost in USD.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Gets or sets the average rating.
    /// </summary>
    [Range(0, 5)]
    public double Rating { get; set; }
}
