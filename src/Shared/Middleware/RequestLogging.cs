using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Shared.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        finally
        {
            logger.LogInformation(
                "{ip} {method} {schema} {host}{path}{query} responded {statusCode} in {elapsed:0.0000} ms",
                    context.Connection.RemoteIpAddress,
                    context.Request.Method,
                    context.Request.Scheme,
                    context.Request.Host,
                    context.Request.Path,
                    context.Request.QueryString,
                    context.Response.StatusCode,
                    watch.Elapsed.TotalMilliseconds);
        }
    }
}