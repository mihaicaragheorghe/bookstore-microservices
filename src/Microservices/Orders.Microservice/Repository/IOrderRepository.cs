using Orders.Microservice.Models;

namespace Orders.Microservice.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
    Task<Order?> GetByIdAsync(string id);
    Task<Order> CreateAsync(Order order);
    Task<bool> UpdateAsync(Order order);
    Task<bool> DeleteAsync(string id);
}