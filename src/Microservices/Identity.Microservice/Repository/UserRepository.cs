using Identity.Microservice.Models;
using MongoDB.Driver;

namespace Identity.Microservice.Repository;

public class UserRepository(IMongoDatabase db) : IUserRepository
{
    private readonly IMongoCollection<User> _users = db.GetCollection<User>(User.CollectionName);

    public async Task<User?> GetUserByIdAsync(string id) =>
        await _users.Find(u => u.Id == id).SingleOrDefaultAsync();

    public async Task<User?> GetUserByEmailAsync(string email) =>
        await _users.Find(u => u.Email == email).SingleOrDefaultAsync();

    public async Task<User> AddUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var result = await _users.DeleteOneAsync(u => u.Id == id);
        return result.DeletedCount > 0;
    }
}