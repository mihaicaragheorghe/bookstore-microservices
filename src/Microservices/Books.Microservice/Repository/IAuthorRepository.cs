using Books.Microservice.Models;

namespace Books.Microservice.Repository;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorAsync(string id);
    Task<Author> AddAuthorAsync(Author author);
    Task<Author?> UpdateAuthorAsync(Author author);
    Task<bool> DeleteAuthorAsync(string id);
}