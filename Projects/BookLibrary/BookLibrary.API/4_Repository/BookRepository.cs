using BookLibrary.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Repository;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _libraryContext;

    public BookRepository(LibraryDbContext context)
    {
        _libraryContext = context;
    }
    
    public Task AddBook(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _libraryContext.Books.Include(b => b.Author).ToListAsync();
    }

    public Task<Book> GetBookById(int id)
    {
        throw new NotImplementedException();
    }
}