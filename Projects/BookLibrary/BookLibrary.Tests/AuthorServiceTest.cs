using System.Runtime.InteropServices;
using BookLibrary.API.Data;
using BookLibrary.API.Repository;
using BookLibrary.API.Service;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace BookLibrary.Tests.Authors;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly IAuthorService _authorService;
    private readonly Mock<IBookRepository> _mockBookRepository;

    public AuthorServiceTests()
    {
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _authorService = new AuthorService(_mockAuthorRepository.Object);
        _mockBookRepository = new Mock<IBookRepository>();
    }

    [Fact]
    public async Task GetAllAuthorsTest()
    {
        //Arrange
        var authors = new List<Author> {
            new Author { Name = "bob" },
            new Author { Name = "alex" },
            new Author { Name = "sam" }
            };
        _mockAuthorRepository.Setup(r => r.GetAllAuthors()).ReturnsAsync(authors);

        //Act
        var result = await _authorService.GetAllAuthors();

        //Assert
        Assert.Equal(authors, result);
    }

    [Fact]
    public async Task GetAuthorById_AuthorNotExist()
    {
        //Arrange
        Author author = null;
        _mockAuthorRepository.Setup(r => r.GetAuthorById(1)).ReturnsAsync(author);

        //Act
        var result = await _authorService.GetAuthorById(1);

        //Assert
        Assert.Equal(result, author);
        _mockAuthorRepository.Verify(r => r.GetAuthorById(1), Times.Once);
    }

    [Fact]
    public async Task GetAuthorById_AuthorExist()
    {
        //Arrange
        var author = new Author { Name = "bob" };
        _mockAuthorRepository.Setup(r => r.GetAuthorById(1)).ReturnsAsync(author);

        //Act
        var result = await _authorService.GetAuthorById(1);

        //Assert
        Assert.Equal(author, result);
    }

    [Fact]
    public async Task AddAuthor_ReturnAddedAuthor()
    {
        //Arrange
        var author = new Author { Name = "bob" };
        _mockAuthorRepository.Setup(r => r.AddAuthor(author)).ReturnsAsync(author);

        //Act
        var result = await _authorService.AddAuthor(author);

        //Assert
        Assert.Equal(author, result);
    }

    [Fact]
    public async Task DeleteAuthorById_AuthorNotExist()
    {
        //Arrange
        var author = 1;
        _mockAuthorRepository.Setup(r => r.DeleteAuthorById(author)).Returns(Task.CompletedTask);

        //Act
        await _authorService.DeleteAuthorById(author);

        //Assert
        _mockAuthorRepository.Verify(r => r.DeleteAuthorById(author), Times.Once);
    }

    [Fact]
    public async Task DeleteAuthorById_AuthorExist()
    {
        //Arrange
        var author = 1;
        _mockAuthorRepository.Setup(r => r.DeleteAuthorById(author)).Returns(Task.CompletedTask);

        //Act
        await _authorService.DeleteAuthorById(author);

        //Assert
        _mockAuthorRepository.Verify(r => r.DeleteAuthorById(author), Times.Once);
    }

    [Fact]
    public async Task AddBookToAuthorById_ValidAuthorAndBook()
    {
        // Arrange
        var authorId = 1;
        var bookId = 1;
        var author = new Author { Id = authorId, Name = "Author1" };
        var book = new Book { Id = bookId, Title = "Book1", ISBN = "123456789" };

        _mockAuthorRepository.Setup(r => r.AddBookToAuthorById(authorId, bookId)).Returns(Task.CompletedTask);

        // Act
        await _authorService.AddBookToAuthorById(authorId, bookId);

        // Assert
        _mockAuthorRepository.Verify(r => r.AddBookToAuthorById(authorId, bookId), Times.Once);
    }

}