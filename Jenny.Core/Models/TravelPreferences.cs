using System.ComponentModel.DataAnnotations;

namespace Jenny.Core.Models;

/// <summary>
/// Represents saved travel preferences for a user.
/// </summary>
public sealed class TravelPreferences
{
    /// <summary>
    /// Gets or sets supported transport types.
    /// </summary>
    public IReadOnlyCollection<TransportType> TransportTypes { get; set; } = new[] { TransportType.Train, TransportType.Ferry };

    /// <summary>
    /// Gets or sets the trip budget in USD.
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal Budget { get; set; } = 250;

    /// <summary>
    /// Gets or sets the preferred pace.
    /// </summary>
    public TravelPace Pace { get; set; } = TravelPace.Balanced;

    /// <summary>
    /// Gets or sets traveler interests.
    /// </summary>
    public IReadOnlyCollection<string> Interests { get; set; } = Array.Empty<string>();
}
