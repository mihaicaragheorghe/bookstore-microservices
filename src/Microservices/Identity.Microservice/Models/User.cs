using MongoDB.Bson.Serialization.Attributes;

namespace Identity.Microservice.Models;

public class User
{
    [BsonId]
    public Guid Id { get; init; }

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    [BsonIgnore]
    internal const string CollectionName = "Users";
}
