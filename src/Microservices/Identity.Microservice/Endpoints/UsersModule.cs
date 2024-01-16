using System.Security.Claims;
using Identity.Microservice.Contracts;
using Identity.Microservice.Models;
using Identity.Microservice.Repository;
using SolON.API.Authentication.Services;

namespace Identity.Microservice.Endpoints;

public static class UsersModule
{
    public static void RegisterUserEndpoints(this WebApplication app)
    {
        app.MapPost("/identity/login", async (LoginRequest request, 
            IUserRepository repository, IJwtTokenGenerator tokenGenerator) =>
        {
            var user = await repository.GetUserAsync(request.Email);

            if (user is null) return Results.NotFound();

            if (!user.Password.Equals(request.Password)) return Results.Unauthorized();

            var token = tokenGenerator.GenerateToken(
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email)
            ]);

            return Results.Ok(new LoginResponse(user.Id, user.Email, user.Name, token));
        })
        .Produces<LoginResponse>()
        .AllowAnonymous();

        app.MapPost("/identity/register", async (RegisterRequest request,
            IUserRepository repository, IJwtTokenGenerator tokenGenerator) =>
        {
            if (await repository.GetUserAsync(request.Email) is not null) return Results.Conflict();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = request.Password,
                Name = request.Name
            };

            var token = tokenGenerator.GenerateToken(
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email)
            ]);

            await repository.AddUserAsync(user);

            return Results.Ok(new RegisterResponse(user.Id, user.Email, user.Name, token));
        })
        .Produces<RegisterResponse>()
        .AllowAnonymous();

        app.MapGet("/identity/validate", (ClaimsPrincipal user) =>
        {
            if (user is null) return Results.Unauthorized();

            return Results.Ok();
        })
        .RequireAuthorization();
    }
}