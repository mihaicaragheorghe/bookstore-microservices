using Identity.Microservice.Models;

namespace Identity.Microservice.Repository;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> AddUserAsync(User user);
    Task<bool> DeleteUserAsync(string id);
}