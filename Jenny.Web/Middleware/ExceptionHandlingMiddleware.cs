using System.Net;

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
            logger.LogWarning(exception, "Validation error while processing request.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                title = "Invalid request.",
                detail = exception.Message,
                status = context.Response.StatusCode
            });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception while processing request.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                title = "Server error.",
                detail = "Jenny could not process the request.",
                status = context.Response.StatusCode
            });
        }
    }
}
