using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Identity.Microservice.Models;

public class User
{
    public static readonly string CollectionName = nameof(User);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string Name { get; init; } = null!;

    public User() { }

    public User(string email, string password, string name)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Email = email;
        Password = password;
        Name = name;
    }
}
