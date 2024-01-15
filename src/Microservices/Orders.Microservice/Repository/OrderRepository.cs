using MongoDB.Driver;
using Orders.Microservice.Models;

namespace Orders.Microservice.Repository;

public class OrderRepository(IMongoClient db) : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders = db.GetDatabase("Orders").GetCollection<Order>("Orders");

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId) =>
        await _orders.Find(order => order.UserId == userId).ToListAsync();

    public async Task<Order?> GetByIdAsync(Guid id) =>
        await _orders.Find(order => order.Id == id).FirstOrDefaultAsync();

    public async Task<Order> CreateAsync(Order order)
    {
        await _orders.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        var result = await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _orders.DeleteOneAsync(order => order.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}