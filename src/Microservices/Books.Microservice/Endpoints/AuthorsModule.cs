using Books.Microservice.Contracts;
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

        app.MapGet("/api/authors/{id}", async (string id, IAuthorRepository repository) =>
        {
            var author = await repository.GetAuthorWithBooksAsync(id);

            return author is null ? Results.NotFound() : Results.Ok(author);
        })
        .RequireAuthorization()
        .Produces<AuthorBooksAggregate>();

        app.MapPost("/api/authors", async (CreateAuthorRequest request, IAuthorRepository repository) =>
        {
            return Results.Ok(await repository.AddAuthorAsync(new Author(request.Name)));
        })
        .RequireAuthorization()
        .Produces<Author>();

        app.MapDelete("/api/authors/{id}", async (string id, IAuthorRepository repository) =>
        {
            var author = await repository.GetAuthorAsync(id);
            
            if (author is null) return Results.NotFound();

            return await repository.DeleteAuthorAsync(id) ? Results.Ok() : Results.Problem();
        })
        .RequireAuthorization();
    }
}