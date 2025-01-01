using BookLibrary.API.Data;
using BookLibrary.API.Repository;

namespace BookLibrary.API.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }

    public async Task AddUser(User user)
    {
        await _userRepository.AddUser(user);
    }

    public async Task DeleteUserById(int id)
    {
        await _userRepository.DeleteUserById(id);
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public Task<User> GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }
}