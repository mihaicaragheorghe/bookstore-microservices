namespace Shared.Authentication.Abstractions;

public interface IUserIdentityProvider
{
    Guid GetUserId();
}