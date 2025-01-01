using BookLibrary.API.Data;

namespace BookLibrary.API.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> AddUser(User user);
    Task DeleteUserById(int id);
}