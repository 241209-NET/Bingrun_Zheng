using BookLibrary.API.Data;
using BookLibrary.API.Repository;
using BookLibrary.API.Service;
using Moq;

namespace BookLibrary.Tests.Books;

public class BookServiceTests
{
    private readonly IBookService _bookService;
    private readonly Mock<IBookRepository> _mockBookRepository;

    public BookServiceTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _bookService = new BookService(_mockBookRepository.Object);
    }

    [Fact]
    public async Task GetAllBooksTest()
    {
        //Arrange
        var books = new List<Book> {
            new Book { Id = 1, Title = "Book1", ISBN = "8797953" },
            new Book { Id = 2, Title = "Book2", ISBN = "64565132" },
            new Book { Id = 3, Title = "Book3", ISBN = "564447894" }
            };
        _mockBookRepository.Setup(r => r.GetAllBooks()).ReturnsAsync(books);

        //Act
        var result = await _bookService.GetAllBooks();

        //Assert
        Assert.Equal(books, result);
    }

    [Fact]
    public async Task GetBookById_BookExists()
    {
        //Arrange
        var book = new Book { Id = 1, Title = "Book1", ISBN = "8797953" };
        _mockBookRepository.Setup(r => r.GetBookById(1)).ReturnsAsync(book);

        //Act
        var result = await _bookService.GetBookById(1);

        //Assert
        Assert.Equal(book, result);
    }

    [Fact]
    public async Task GetBookById_BookDoesNotExist()
    {
        //Arrange
        Book book = null;
        _mockBookRepository.Setup(r => r.GetBookById(1)).ReturnsAsync(book);

        //Act
        var result = await _bookService.GetBookById(1);

        //Assert
        Assert.Equal(book, result);
    }

    [Fact]
    public async Task AddBookTest()
    {
        // Arrange
        var book = new Book { Id = 1, Title = "Book1", ISBN = "8797953" };

        _mockBookRepository.Setup(r => r.AddBook(book)).ReturnsAsync(book);

        //Act
        var result = await _bookService.AddBook(book);

        //Assert
        Assert.Equal(book, result);
        Assert.NotNull(result);
        _mockBookRepository.Verify(r => r.AddBook(book), Times.Once);
    }

    [Fact]
    public async Task DeleteBookByIdTestTest()
    {
        //Arrange
        var bookIdToDelete = 1;
        _mockBookRepository.Setup(r => r.DeleteBookById(1)).Returns(Task.CompletedTask);

        //Act
        await _bookService.DeleteBookById(bookIdToDelete);

        //Assert
        _mockBookRepository.Verify(r => r.DeleteBookById(bookIdToDelete), Times.Once);
    }
}