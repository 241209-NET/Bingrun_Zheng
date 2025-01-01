using BookLibrary.API.Data;

namespace BookLibrary.API.Repository;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author> GetAuthorById(int id);
    Task<Author> AddAuthor(Author author);
    Task DeleteAuthorById(int id);
    Task AddBookToAuthorById(int authorId, int bookId);
}