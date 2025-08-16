using System.Diagnostics;

namespace Hostal.Api.Middlewares.RequestLoggingMiddleware;

public class RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger):IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();
        if (stopWatch.ElapsedMilliseconds > 4000)
        {
            logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms", context.Request.Method,
                context.Request.Path, stopWatch.ElapsedMilliseconds);
            
        }
    }
}