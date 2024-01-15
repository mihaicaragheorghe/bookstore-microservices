using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Microservice.Models;

public class Book
{
    [BsonId]
    public Guid Id { get; init; }

    [BsonRepresentation(BsonType.ObjectId)]
    public Guid AuthorId { get; init; }
    
    public string Title { get; init; } = string.Empty;

    [BsonIgnore]
    internal const string CollectionName = "Books";
}