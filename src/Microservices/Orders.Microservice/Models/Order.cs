using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Orders.Microservice.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; init; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string BookId { get; init; } = null!;
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Order() { }

    public Order(string userId, string bookId)
    {
        Id = ObjectId.GenerateNewId().ToString();
        UserId = userId;
        BookId = bookId;
    }
}