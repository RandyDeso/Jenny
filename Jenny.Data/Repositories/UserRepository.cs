using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory user repository.
/// </summary>
public sealed class UserRepository(MockTravelDataStore store) : IUserRepository
{
    /// <inheritdoc />
    public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        store.Users.TryGetValue(userId, out var user);
        return Task.FromResult(user);
    }

    /// <inheritdoc />
    public Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        store.Users[user.UserId] = user;
        return Task.CompletedTask;
    }
}
