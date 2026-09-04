using System.Collections.Concurrent;

namespace Jenny.Data.Infrastructure;

/// <summary>
/// Provides keyed asynchronous locking.
/// </summary>
public sealed class KeyedStateLock
{
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

    /// <summary>
    /// Executes an operation under a key-specific semaphore.
    /// </summary>
    public async Task<T> ExecuteAsync<T>(Guid key, Func<Task<T>> action)
    {
        var semaphore = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();

        try
        {
            return await action();
        }
        finally
        {
            semaphore.Release();
        }
    }
}
