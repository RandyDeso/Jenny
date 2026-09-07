using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Jenny.Tests;

public sealed class ChatApiTests : IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> factory = new();

    [Fact]
    public async Task EmptyMessage_ReturnsValidationProblem()
    {
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/chat", new { message = "   " });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(payload);
        Assert.NotNull(payload.Errors);
        Assert.Contains("message", payload.Errors.Keys);
    }

    public ValueTask DisposeAsync()
    {
        factory.Dispose();
        return ValueTask.CompletedTask;
    }
}
