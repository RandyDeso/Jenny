namespace Jenny.Core.Abstractions;

/// <summary>
/// Provides travel tip data access.
/// </summary>
public interface ITravelTipRepository
{
    /// <summary>
    /// Gets travel tips for a location when available.
    /// </summary>
    Task<IReadOnlyCollection<string>> GetTipsAsync(Guid? locationId = null, CancellationToken cancellationToken = default);
}
