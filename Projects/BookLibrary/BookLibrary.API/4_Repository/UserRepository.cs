using BookLibrary.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly LibraryDbContext _libraryContext;

    public UserRepository(LibraryDbContext context)
    {
        _libraryContext = context;
    }

    public async Task<User> AddUser(User user)
    {
        _libraryContext.Add(user);
        await _libraryContext.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserById(int id)
    {
        var user = await _libraryContext.Users.FindAsync(id);
        if (user != null)
        {
            _libraryContext.Users.Remove(user);
            await _libraryContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _libraryContext.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _libraryContext.Users.FindAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} do not exist.");
        }
        return user;
    }
}