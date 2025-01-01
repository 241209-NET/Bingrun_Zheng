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
    public async Task<Book> AddBook(Book book){
        await _bookRepoitory.AddBook(book);
        return book;
    }
    public async Task DeleteBookById(int id)
    {
        await _bookRepoitory.DeleteBookById(id);
    }

    public async Task<IEnumerable<Book>> GetAllBooks(){
        return await _bookRepoitory.GetAllBooks();
    }
    
    public async Task<Book> GetBookById(int id)
    {
        return await _bookRepoitory.GetBookById(id);
    }
    
}