using BookLibrary.API.Data;

namespace BookLibrary.API.Service;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author> GetAuthorById(int id);
    Task<Author> AddAuthor(Author author);
    Task DeleteAuthorById(int id);
    Task AddBookToAuthorById(int authorId, int bookId);
}