using Books.Microservice.Models;
using Books.Microservice.Repository;

namespace Books.Microservice.Endpoints;

public static class BooksModule
{
    public static void RegisterBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", async (IBookRepository repository) =>
        {
            return Results.Ok(await repository.GetBooksAsync());
        });
        
        app.MapGet("/books/{id}", async (Guid id, IBookRepository repository) =>
        {
            var book = await repository.GetBookAsync(id);

            return book is null ? Results.NotFound() : Results.Ok(book);
        });

        app.MapGet("/books/author/{id}", async (Guid id, IBookRepository repository) =>
        {
            var books = await repository.GetBooksAsync(id);

            return books is null ? Results.NotFound() : Results.Ok(books);
        });

        app.MapPost("/books", async (Book book, IBookRepository repository) =>
        {
            return Results.Ok(await repository.AddBookAsync(book));
        });

        app.MapDelete("/books/{id}", async (Guid id, IBookRepository repository) =>
        {
            var book = await repository.GetBookAsync(id);
            
            if (book is null) return Results.NotFound();

            return await repository.DeleteBookAsync(id) ? Results.Ok() : Results.Problem();
        });
    }
}