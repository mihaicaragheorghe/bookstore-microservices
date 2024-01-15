using MongoDB.Bson.Serialization.Attributes;

namespace Books.Microservice.Models;

public class Author
{
    [BsonId]
    public Guid Id { get; init; }
    
    public string Name { get; init; } = string.Empty;

    [BsonIgnore]
    internal const string CollectionName = "Authors";
}