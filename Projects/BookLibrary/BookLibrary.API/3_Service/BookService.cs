using BookLibrary.API.Data;
using BookLibrary.API.Repository;

namespace BookLibrary.API.Service;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepoitory;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepoitory = bookRepository;
    }
    public Task AddBook(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Book>> GetAllBooks() => await _bookRepoitory.GetAllBooks();
    
    public Task<Book> GetBookById(int id)
    {
        throw new NotImplementedException();
    }
}