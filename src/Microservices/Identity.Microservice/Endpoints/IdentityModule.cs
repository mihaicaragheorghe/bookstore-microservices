using System.Security.Claims;
using Identity.Microservice.Contracts;
using Identity.Microservice.Models;
using Identity.Microservice.Repository;
using Shared.Abstractions;

namespace Identity.Microservice.Endpoints;

public static class IdentityModule
{
    public static void RegisterIdentityEndpoints(this WebApplication app)
    {
        app.MapPost("/api/identity/login", async (LoginRequest request, 
            IUserRepository repository, IJwtBuilder jwtBuilder) =>
        {
            var user = await repository.GetUserByEmailAsync(request.Email);

            if (user is null) return Results.NotFound();

            if (!user.Password.Equals(request.Password)) return Results.Unauthorized();

            var token = jwtBuilder.Build(user.Id.ToString());

            return Results.Ok(new LoginResponse(user.Id, user.Email, user.Name, token));
        })
        .Produces<LoginResponse>()
        .AllowAnonymous();

        app.MapPost("/api/identity/register", async (RegisterRequest request,
            IUserRepository repository, IJwtBuilder jwtBuilder) =>
        {
            if (await repository.GetUserByEmailAsync(request.Email) is not null) return Results.Conflict();

            var user = new User(request.Email, request.Password, request.Name);

            var token = jwtBuilder.Build(user.Id.ToString());

            await repository.AddUserAsync(user);

            return Results.Ok(new RegisterResponse(user.Id, user.Email, user.Name, token));
        })
        .Produces<RegisterResponse>()
        .AllowAnonymous();

        app.MapGet("/api/identity/validate", async (string email, string token, 
            IUserRepository userRepository, IJwtBuilder jwtBuilder) =>
        {
            var user = await userRepository.GetUserByEmailAsync(email);

            if (user is null) return Results.NotFound();

            var userId = jwtBuilder.ValidateToken(token);

            if (userId != user.Id.ToString()) return Results.BadRequest("The token is invalid");

            return Results.Ok(userId);
        })
        .RequireAuthorization();
    }
}