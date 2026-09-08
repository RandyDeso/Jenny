using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory conversation repository.
/// </summary>
public sealed class ConversationRepository(MockTravelDataStore store) : IConversationRepository
{
    /// <inheritdoc />
    public Task<Conversation?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        store.Conversations.TryGetValue(userId, out var conversation);
        return Task.FromResult(conversation);
    }

    /// <inheritdoc />
    public Task SaveAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        store.Conversations[conversation.UserId] = conversation;
        return Task.CompletedTask;
    }
}
