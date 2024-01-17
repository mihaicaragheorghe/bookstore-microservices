using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Microservice.Models;

public class Book
{
    public static readonly string CollectionName = nameof(Book);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; init; } = null!;

    public string Title { get; init; } = null!;

    public string Description { get; init; } = null!;
    
    public Book() { }

    public Book(string authorId, string title, string description)
    {
        Id = ObjectId.GenerateNewId().ToString();
        AuthorId = authorId;
        Title = title;
        Description = description;
    }
}
