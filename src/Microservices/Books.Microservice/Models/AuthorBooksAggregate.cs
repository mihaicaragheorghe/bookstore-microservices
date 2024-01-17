using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Microservice.Models;

public class AuthorBooksAggregate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = new();

    public AuthorBooksAggregate() { }

    public AuthorBooksAggregate(Author author, List<Book> books)
    {
        Id = author.Id;
        Name = author.Name;
        Books = books;
    }
}