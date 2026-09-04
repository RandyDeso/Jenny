using Jenny.Core.Abstractions;

namespace Jenny.Data.Infrastructure;

/// <summary>
/// Serializes user state updates.
/// </summary>
public sealed class UserStateLock(KeyedStateLock keyedStateLock) : IUserStateLock
{
    /// <inheritdoc />
    public Task<T> ExecuteAsync<T>(Guid userId, Func<Task<T>> action) => keyedStateLock.ExecuteAsync(userId, action);
}
