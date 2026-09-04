using System.Diagnostics;
using Jenny.Contracts;
using Jenny.Data;
using Jenny.Infrastructure;
using Jenny.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddSingleton<TravelData>();
builder.Services.AddSingleton<ITravelAssistantService, TravelAssistantService>();

var app = builder.Build();

app.UseExceptionHandler();
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("Jenny.Requests");
    var stopwatch = Stopwatch.StartNew();

    await next();

    stopwatch.Stop();
    logger.LogInformation(
        "{Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
        context.Request.Method,
        context.Request.Path,
        context.Response.StatusCode,
        stopwatch.ElapsedMilliseconds);
});
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/chat", (ChatRequest request, ITravelAssistantService travelAssistantService, ILogger<Program> logger) =>
{
    if (string.IsNullOrWhiteSpace(request.Message))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["message"] = ["Please enter a travel question before sending."]
        });
    }

    var response = travelAssistantService.GetResponse(request.Message);
    logger.LogInformation("Completed chat request with intent {Intent}", response.Intent);
    return Results.Ok(response);
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program;
