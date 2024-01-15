using Books.Microservice.Models;
using Books.Microservice.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Books.Microservice.Endpoints;

public static class AuthorsModule
{
    public static void RegisterAuthorEndpoints(this WebApplication app)
    {
        app.MapGet("/authors", async (IAuthorRepository repository) =>
        {
            return Results.Ok(await repository.GetAuthorsAsync());
        });

        app.MapGet("/authors/{id}", async (Guid id, IAuthorRepository repository) =>
        {
            var author = await repository.GetAuthorAsync(id);

            return author is null ? Results.NotFound() : Results.Ok(author);
        });

        app.MapPost("/authors", async (Author author, IAuthorRepository repository) =>
        {
            return Results.Ok(await repository.AddAuthorAsync(author));
        });

        app.MapDelete("/authors/{id}", async (Guid id, IAuthorRepository repository) =>
        {
            var author = await repository.GetAuthorAsync(id);
            
            if (author is null) return Results.NotFound();

            return await repository.DeleteAuthorAsync(id) ? Results.Ok() : Results.Problem();
        });
    }
}