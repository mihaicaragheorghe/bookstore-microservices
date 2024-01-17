using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Shared.Abstractions;

namespace Shared.Middleware;

public class JwtMiddleware(IJwtBuilder jwtBuilder) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = await context.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");

        if (string.IsNullOrWhiteSpace(token))
        {
            await HandleUnauthorized(context);
            return;
        }

        var userId = jwtBuilder.ValidateToken(token);

        if (ObjectId.TryParse(userId, out _))
        {
            await HandleUnauthorized(context);
            return;
        }

        context.Items["UserId"] = userId;

        await next(context);
    }

    private static async Task HandleUnauthorized(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await context.Response.WriteAsJsonAsync("Unauthorized");
    }
}
