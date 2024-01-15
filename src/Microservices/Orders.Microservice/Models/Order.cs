using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Orders.Microservice.Models;

public class Order
{
    [BsonId]
    public Guid Id { get; init; }

    [BsonRepresentation(BsonType.ObjectId)]
    public Guid UserId { get; init; }

    [BsonRepresentation(BsonType.ObjectId)]
    public Guid BookId { get; init; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}