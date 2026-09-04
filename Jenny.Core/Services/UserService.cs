using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Implements user preference and favorites behavior.
/// </summary>
public sealed class UserService(IUserRepository userRepository) : IUserService
{
    /// <inheritdoc />
    public async Task<UserFavorite> AddFavoriteAsync(Guid userId, UserFavorite favorite, CancellationToken cancellationToken = default)
    {
        var user = await GetOrCreateAsync(userId, cancellationToken: cancellationToken);
        favorite.Id = favorite.Id == Guid.Empty ? Guid.NewGuid() : favorite.Id;
        favorite.UserId = userId;
        favorite.CreatedAt = favorite.CreatedAt == default ? DateTimeOffset.UtcNow : favorite.CreatedAt;
        user.Favorites.Add(favorite);
        await userRepository.SaveAsync(user, cancellationToken);
        return favorite;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<UserFavorite>> GetFavoritesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await GetOrCreateAsync(userId, cancellationToken: cancellationToken);
        return user.Favorites.OrderByDescending(favorite => favorite.CreatedAt).ToArray();
    }

    /// <inheritdoc />
    public async Task<User> GetOrCreateAsync(Guid userId, string? email = null, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is not null)
        {
            return user;
        }

        user = new User
        {
            UserId = userId,
            Email = string.IsNullOrWhiteSpace(email) ? $"traveler+{userId:N}@jenny.local" : email,
            Preferences = new TravelPreferences()
        };

        await userRepository.SaveAsync(user, cancellationToken);
        return user;
    }
}
