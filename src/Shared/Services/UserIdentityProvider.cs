using Microsoft.AspNetCore.Http;
using Shared.Abstractions;

namespace Shared.Services;

public class UserIdentityProvider(IHttpContextAccessor httpContextAccessor) 
    : IUserIdentityProvider
{
    public Guid GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException("User identity not found");
        }

        return Guid.Parse(userId);
    }
}
