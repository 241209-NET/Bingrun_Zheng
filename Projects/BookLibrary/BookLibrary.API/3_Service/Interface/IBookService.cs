using BookLibrary.API.Data;

namespace BookLibrary.API.Service;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book> GetBookById(int id);
    Task AddBook(Book book);
    Task DeleteBookById(int id);
}