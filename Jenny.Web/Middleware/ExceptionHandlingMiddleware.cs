using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Middleware;

/// <summary>
/// Converts unhandled exceptions into problem details responses.
/// </summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>
    /// Executes the middleware.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ArgumentException exception)
        {
            if (context.Response.HasStarted)
            {
                logger.LogWarning(exception, "Validation error occurred after the response started.");
                throw;
            }

            logger.LogWarning(exception, "Validation error while processing request.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await WriteProblemDetailsAsync(context, "Invalid request.", exception.Message);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                logger.LogError(exception, "Unhandled exception occurred after the response started.");
                throw;
            }

            logger.LogError(exception, "Unhandled exception while processing request.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await WriteProblemDetailsAsync(context, "Server error.", "Jenny could not process the request.");
        }
    }

    private static Task WriteProblemDetailsAsync(HttpContext context, string title, string detail)
    {
        context.Response.ContentType = "application/problem+json";
        return context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = context.Response.StatusCode
        });
    }
}
