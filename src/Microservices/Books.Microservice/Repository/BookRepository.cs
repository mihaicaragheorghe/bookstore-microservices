using Books.Microservice.Models;
using MongoDB.Driver;

namespace Books.Microservice.Repository;

public class BookRepository(IMongoClient db) : IBookRepository
{
    private readonly IMongoCollection<Book> _books = db.GetDatabase("Books").GetCollection<Book>(Book.CollectionName);

    public async Task<IEnumerable<Book>> GetBooksAsync() =>
        await _books.Find(book => true).ToListAsync();

    public async Task<Book?> GetBookAsync(string id) =>
        await _books.Find(book => book.Id == id).SingleOrDefaultAsync();

    public async Task<IEnumerable<Book>> GetBooksAsync(string authorId) =>
        await _books.Find(book => book.AuthorId == authorId).ToListAsync();

    public async Task<Book> AddBookAsync(Book book)
    {
        await _books.InsertOneAsync(book);
        return book;
    }

    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var result = await _books.ReplaceOneAsync(b => b.Id == book.Id, book);
        return result.IsAcknowledged ? book : null;
    }

    public async Task<bool> DeleteBookAsync(string id)
    {
        var result = await _books.DeleteOneAsync(book => book.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}