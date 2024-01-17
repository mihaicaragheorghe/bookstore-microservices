using Books.Microservice.Models;

namespace Books.Microservice.Repository;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book?> GetBookAsync(string id);
    Task<IEnumerable<Book>> GetBooksAsync(string authorId);
    Task<Book> AddBookAsync(Book book);
    Task<Book?> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(string id);
}