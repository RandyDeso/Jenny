using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides user preference and favorite management.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets or creates a user profile.
    /// </summary>
    Task<User> GetOrCreateAsync(Guid userId, string? email = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets favorites for a user.
    /// </summary>
    Task<IReadOnlyCollection<UserFavorite>> GetFavoritesAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a favorite for a user.
    /// </summary>
    Task<UserFavorite> AddFavoriteAsync(Guid userId, UserFavorite favorite, CancellationToken cancellationToken = default);
}
