using BookLibrary.API.Data;

namespace BookLibrary.API.Service;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task AddUser(User user);
    Task DeleteUserById(int id);
}