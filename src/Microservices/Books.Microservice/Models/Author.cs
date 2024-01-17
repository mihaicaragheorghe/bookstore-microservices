using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Microservice.Models;

public class Author
{
    public static readonly string CollectionName = nameof(Author);

    [BsonId]
    public string Id { get; init; } = null!;

    public string Name { get; init; } = string.Empty;

    public Author() { }

    public Author(string name)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
    }

    public Author(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
