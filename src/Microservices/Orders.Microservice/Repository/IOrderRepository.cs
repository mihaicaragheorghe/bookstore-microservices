using Orders.Microservice.Models;

namespace Orders.Microservice.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order> CreateAsync(Order order);
    Task<bool> UpdateAsync(Order order);
    Task<bool> DeleteAsync(Guid id);
}