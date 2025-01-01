using BookLibrary.API.Data;
using BookLibrary.API.Repository;
using BookLibrary.API.Service;
using Moq;

namespace BookLibrary.Tests.Users;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task GetAllUsers()
    {
        //Arrange
        var users = new List<User>{
            new User{ Id = 1, Name = "Bob", Role = "user" },
            new User{ Id = 2, Name = "Alex", Role = "user" },
            new User{ Id = 3, Name = "Sam", Role = "user" }
        };

        _mockUserRepository.Setup(r => r.GetAllUsers()).ReturnsAsync(users);

        //Act
        var result = await _userService.GetAllUsers();

        //Assert
        Assert.Equal(users, result);
    }

    [Fact]
    public async Task GetUserById_UserNotExist()
    {
        //Arrange
        User user = null;
        _mockUserRepository.Setup(r => r.GetUserById(1)).ReturnsAsync(user);

        //Act
        var result = await _userService.GetUserById(1);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserById_UserExist()
    {
        //Arrange
        var user = new User{ Id = 1, Name = "Bob", Role = "user" };
        _mockUserRepository.Setup(r => r.GetUserById(1)).ReturnsAsync(user);

        //Act
        var result = await _userService.GetUserById(1);

        //Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task AddUser_ReturnAddedUser(){
        //Arrange
        var user = new User{ Id = 1, Name = "Bob", Role = "user" };
        
        _mockUserRepository.Setup(r => r.AddUser(user)).ReturnsAsync(user);

        //Act
        await _userService.AddUser(user);

        //Assert
        _mockUserRepository.Verify(r => r.AddUser(user), Times.Once);
    }

    [Fact]
    public async Task DeleteUserByIdTest(){
        //Arrange
        var user = 1;
        _mockUserRepository.Setup(r => r.DeleteUserById(1)).Returns(Task.CompletedTask);

        //Act
        await _userService.DeleteUserById(user);

        //Assert
        _mockUserRepository.Verify(r => r.DeleteUserById(user), Times.Once);
    }

}