using Jenny.Core.Abstractions;

namespace Jenny.Data.Infrastructure;

/// <summary>
/// Serializes conversation state updates.
/// </summary>
public sealed class ConversationStateLock(KeyedStateLock keyedStateLock) : IConversationStateLock
{
    /// <inheritdoc />
    public Task<T> ExecuteAsync<T>(Guid userId, Func<Task<T>> action) => keyedStateLock.ExecuteAsync(userId, action);
}
