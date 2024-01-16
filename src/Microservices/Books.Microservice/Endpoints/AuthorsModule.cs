using Books.Microservice.Models;
using Books.Microservice.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Books.Microservice.Endpoints;

public static class AuthorsModule
{
    public static void RegisterAuthorEndpoints(this WebApplication app)
    {
        app.MapGet("/api/authors", async (IAuthorRepository repository) =>
        {
            return Results.Ok(await repository.GetAuthorsAsync());
        })
        .RequireAuthorization()
        .Produces<IEnumerable<Author>>();

        app.MapGet("/api/authors/{id}", async (Guid id, IAuthorRepository repository) =>
        {
            var author = await repository.GetAuthorAsync(id);

            return author is null ? Results.NotFound() : Results.Ok(author);
        })
        .RequireAuthorization()
        .Produces<Author>();

        app.MapPost("/api/authors", async (Author author, IAuthorRepository repository) =>
        {
            return Results.Ok(await repository.AddAuthorAsync(author));
        })
        .RequireAuthorization()
        .Produces<Author>();

        app.MapDelete("/api/authors/{id}", async (Guid id, IAuthorRepository repository) =>
        {
            var author = await repository.GetAuthorAsync(id);
            
            if (author is null) return Results.NotFound();

            return await repository.DeleteAuthorAsync(id) ? Results.Ok() : Results.Problem();
        })
        .RequireAuthorization();
    }
}