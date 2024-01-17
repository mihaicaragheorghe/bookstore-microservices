using Books.Microservice.Models;
using MongoDB.Driver;

namespace Books.Microservice.Repository;

public class AuthorRepository(IMongoDatabase db) : IAuthorRepository
{
    private readonly IMongoCollection<Author> _authors = db.GetCollection<Author>(Author.CollectionName);

    public async Task<IEnumerable<Author>> GetAuthorsAsync() =>
        await _authors.Find(author => true).ToListAsync();

    public async Task<Author?> GetAuthorAsync(string id) =>
        await _authors.Find(author => author.Id == id).SingleOrDefaultAsync();

    public async Task<Author> AddAuthorAsync(Author author)
    {
        await _authors.InsertOneAsync(author);
        return author;
    }

    public async Task<Author?> UpdateAuthorAsync(Author author)
    {
        var result = await _authors.ReplaceOneAsync(a => a.Id == author.Id, author);
        return result.IsAcknowledged ? author : null;
    }

    public async Task<bool> DeleteAuthorAsync(string id)
    {
        var result = await _authors.DeleteOneAsync(author => author.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}