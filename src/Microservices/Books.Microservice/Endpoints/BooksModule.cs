using Books.Microservice.Models;
using Books.Microservice.Repository;

namespace Books.Microservice.Endpoints;

public static class BooksModule
{
    public static void RegisterBookEndpoints(this WebApplication app)
    {
        app.MapGet("/api/books", async (IBookRepository repository) =>
        {
            return Results.Ok(await repository.GetBooksAsync());
        })
        .RequireAuthorization()
        .Produces<IEnumerable<Book>>();
        
        app.MapGet("/api/books/{id}", async (Guid id, IBookRepository repository) =>
        {
            var book = await repository.GetBookAsync(id);

            return book is null ? Results.NotFound() : Results.Ok(book);
        })
        .RequireAuthorization()
        .Produces<Book>();

        app.MapGet("/api/books/author/{id}", async (Guid id, IBookRepository repository) =>
        {
            var books = await repository.GetBooksAsync(id);

            return books is null ? Results.NotFound() : Results.Ok(books);
        })
        .RequireAuthorization()
        .Produces<IEnumerable<Book>>();

        app.MapPost("/api/books", async (Book book, IBookRepository repository) =>
        {
            return Results.Ok(await repository.AddBookAsync(book));
        })
        .RequireAuthorization()
        .Produces<Book>();

        app.MapDelete("/api/books/{id}", async (Guid id, IBookRepository repository) =>
        {
            var book = await repository.GetBookAsync(id);
            
            if (book is null) return Results.NotFound();

            return await repository.DeleteBookAsync(id) ? Results.Ok() : Results.Problem();
        })
        .RequireAuthorization();
    }
}