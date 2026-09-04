using Jenny.Core.Models;

namespace Jenny.Core.Abstractions;

/// <summary>
/// Provides user data access.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a user by identifier.
    /// </summary>
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves a user.
    /// </summary>
    Task SaveAsync(User user, CancellationToken cancellationToken = default);
}
