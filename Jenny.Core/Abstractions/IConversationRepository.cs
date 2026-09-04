using Jenny.Core.Models;

namespace Jenny.Core.Abstractions;

/// <summary>
/// Provides conversation persistence.
/// </summary>
public interface IConversationRepository
{
    /// <summary>
    /// Gets a conversation for a user.
    /// </summary>
    Task<Conversation?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves a conversation.
    /// </summary>
    Task SaveAsync(Conversation conversation, CancellationToken cancellationToken = default);
}
