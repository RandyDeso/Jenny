using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Jenny.Tests;

public sealed class ApiValidationTests : IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _factory = new();

    [Fact]
    public async Task ChatHistory_WithEmptyUserId_ReturnsValidationProblem()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/chat/history?userId=00000000-0000-0000-0000-000000000000");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(payload);
        Assert.Contains("userId", payload.Errors.Keys);
    }

    [Fact]
    public async Task Favorites_WithEmptyUserId_ReturnsValidationProblem()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/favorites?userId=00000000-0000-0000-0000-000000000000");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(payload);
        Assert.Contains("userId", payload.Errors.Keys);
    }

    public ValueTask DisposeAsync()
    {
        _factory.Dispose();
        return ValueTask.CompletedTask;
    }
}
