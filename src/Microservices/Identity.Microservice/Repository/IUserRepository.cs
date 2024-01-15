using Identity.Microservice.Models;

namespace Identity.Microservice.Repository;

public interface IUserRepository
{
    Task<User?> GetUserAsync(Guid id);
    Task<User?> GetUserAsync(string email);
    Task<User> AddUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid id);
}