namespace Jenny.Core.Abstractions;

/// <summary>
/// Serializes access to user-scoped state.
/// </summary>
public interface IUserStateLock
{
    /// <summary>
    /// Executes an operation under a user-specific lock.
    /// </summary>
    Task<T> ExecuteAsync<T>(Guid userId, Func<Task<T>> action);
}
