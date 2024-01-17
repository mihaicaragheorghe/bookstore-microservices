namespace Shared.Abstractions;

public interface IUserIdentityProvider
{
    Guid GetUserId();
}