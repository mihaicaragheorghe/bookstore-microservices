using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Authentication.Abstractions;
using Shared.Authentication.Models;

namespace Shared.Authentication.Services;

public class JwtBuilder(IOptions<JwtOptions> options) : IJwtBuilder
{
    private readonly JwtOptions _options = options.Value;

    public string Build(string userId)
    {
        SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(_options.Secret));
        SigningCredentials signingCredentials = new(signingKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.UniqueName, userId),
        ];
        var expirationTime = DateTime.Now.AddMinutes(_options.ExpirationMinutes);

        var jwt = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expirationTime
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string? ValidateToken(string token)
    {
        var principal = GetPrincipalFromToken(token);

        return principal?.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token);
            if (jwtToken is not JwtSecurityToken securityToken)
                return null;

            var key = Encoding.UTF8.GetBytes(_options.Secret);
            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}