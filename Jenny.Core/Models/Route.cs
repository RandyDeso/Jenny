using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents a train or ferry route between two locations.
/// </summary>
public sealed class Route
{
    /// <summary>
    /// Gets or sets the route identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the origin location.
    /// </summary>
    [Required]
    public Location From { get; set; } = new();

    /// <summary>
    /// Gets or sets the destination location.
    /// </summary>
    [Required]
    public Location To { get; set; } = new();

    /// <summary>
    /// Gets or sets the transport type.
    /// </summary>
    public TransportType TransportType { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost in USD.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Gets or sets the notable stops.
    /// </summary>
    public IReadOnlyCollection<string> Stops { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the route difficulty.
    /// </summary>
    public RouteDifficulty Difficulty { get; set; }
}
