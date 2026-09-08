using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides conversational travel assistance.
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Processes a user message.
    /// </summary>
    Task<ChatResponse> ProcessMessageAsync(Guid userId, string message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets chat history for a user.
    /// </summary>
    Task<Conversation> GetHistoryAsync(Guid userId, CancellationToken cancellationToken = default);
}
