using BookLibrary.API.Data;

namespace BookLibrary.API.Repository;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book> GetBookById(int id);
    Task<Book> AddBook(Book book);
    Task DeleteBookById(int id);
}