namespace Shared.Authentication.Abstractions
{
    public interface IJwtBuilder
    {
        string Build(string userId);
        string? ValidateToken(string token);
    }
}