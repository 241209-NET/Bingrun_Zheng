using BookLibrary.API.Data;
using BookLibrary.API.Repository;
using BookLibrary.API.Service;
using Moq;

namespace BookLibrary.Tests.Borrowings;

public class BorrowingServiceTest
{
    private readonly Mock<IBorrowingRepository> _mockBorrowingRepository;
    private readonly IBorrowingService _borrowingService;

    public BorrowingServiceTest()
    {
        _mockBorrowingRepository = new Mock<IBorrowingRepository>();
        _borrowingService = new BorrowingService(_mockBorrowingRepository.Object);
    }

    [Fact]
    public async Task GetAllBorrowings_ReturnsAllBorrowings()
    {
        // Arrange
        var borrowings = new List<Borrowing>
        {
            new Borrowing { Id = 1, UserId = 1, BookId = 1, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) },
            new Borrowing { Id = 2, UserId = 2, BookId = 2, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) }
        };
        _mockBorrowingRepository.Setup(r => r.GetAllBorrowings()).ReturnsAsync(borrowings);

        // Act
        var result = await _borrowingService.GetAllBorrowings();

        // Assert
        Assert.Equal(borrowings.Count, result.Count());
        Assert.Equal(borrowings, result);
    }

    [Fact]
    public async Task GetBorrowingById_BorrowingExist()
    {
        //Arrange
        var borrowing = new Borrowing { Id = 1, UserId = 1, BookId = 1, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) };
        _mockBorrowingRepository.Setup(r => r.GetBorrowingById(1)).ReturnsAsync(borrowing);

        //Act
        var result = await _borrowingService.GetBorrowingById(1);

        //Assert
        Assert.Equal(borrowing, result);
    }

    [Fact]
    public async Task GetBorrowingById_BorrowingNotExist()
    {
        //Arrange
        var borrowing = 1;
        _mockBorrowingRepository.Setup(r => r.GetBorrowingById(borrowing)).ThrowsAsync(new KeyNotFoundException($"Borrowing with id {borrowing} does not exist"));

        //Act,Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _borrowingService.GetBorrowingById(borrowing));
    }

        [Fact]
    public async Task AddBorrowing_BookAvailable()
    {
        // Arrange
        var borrowing = new Borrowing { UserId = 1, BookId = 1 };
        var addedBorrowing = new Borrowing { Id = 1, UserId = 1, BookId = 1, BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) };
        _mockBorrowingRepository.Setup(r => r.AddBorrowing(borrowing)).ReturnsAsync(addedBorrowing);

        // Act
        var result = await _borrowingService.AddBorrowing(borrowing);

        // Assert
        Assert.Equal(addedBorrowing, result);
    }

    [Fact]
    public async Task DeleteBorrowingById_BorrowingExists()
    {
        // Arrange
        var borrowingId = 1;
        _mockBorrowingRepository.Setup(r => r.DeleteBorrowingById(borrowingId)).Returns(Task.CompletedTask);

        // Act
        await _borrowingService.DeleteBorrowingById(borrowingId);

        // Assert
        _mockBorrowingRepository.Verify(r => r.DeleteBorrowingById(borrowingId), Times.Once);
    }

    [Fact]
    public async Task ReturnBook_BorrowingExists()
    {
        // Arrange
        var borrowingId = 1;
        _mockBorrowingRepository.Setup(r => r.ReturnBook(borrowingId)).Returns(Task.CompletedTask);

        // Act
        await _borrowingService.ReturnBook(borrowingId);

        // Assert
        _mockBorrowingRepository.Verify(r => r.ReturnBook(borrowingId), Times.Once);
    }
}