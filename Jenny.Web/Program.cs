using System.Text.Json.Serialization;
using Jenny.Data.Extensions;
using Jenny.Web.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuredOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        if (configuredOrigins.Length > 0)
        {
            policy.WithOrigins(configuredOrigins).AllowAnyHeader().AllowAnyMethod();
            return;
        }

        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Jenny Travel Assistant API",
        Version = "v1",
        Description = "MVP backend for train and ferry travel planning."
    });

    foreach (var xmlFile in new[] { "Jenny.Web.xml", "Jenny.Core.xml", "Jenny.Data.xml" })
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});

builder.Services.AddJennyBackend();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("Default");
app.UseSwagger();
app.UseSwaggerUI();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/api/health", () => Results.Ok(new
{
    Name = "Jenny Travel Assistant API",
    Status = "Ready"
}));

app.Run();

public partial class Program;
