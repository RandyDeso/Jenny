using Jenny.Core.Services.Interfaces;
using Jenny.Web.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Controllers;

/// <summary>
/// Provides chat endpoints.
/// </summary>
[ApiController]
[Route("api/chat")]
public sealed class ChatController(IChatService chatService) : ControllerBase
{
    /// <summary>
    /// Sends a chat message to Jenny.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request, CancellationToken cancellationToken)
    {
        var response = await chatService.ProcessMessageAsync(request.UserId, request.Message, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Gets chat history for a user.
    /// </summary>
    [HttpGet("history")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistory([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var history = await chatService.GetHistoryAsync(userId, cancellationToken);
        return Ok(history);
    }
}
