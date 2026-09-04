namespace Jenny.Core.Abstractions;

/// <summary>
/// Serializes access to conversation-scoped state.
/// </summary>
public interface IConversationStateLock
{
    /// <summary>
    /// Executes an operation under a conversation-specific lock.
    /// </summary>
    Task<T> ExecuteAsync<T>(Guid userId, Func<Task<T>> action);
}
