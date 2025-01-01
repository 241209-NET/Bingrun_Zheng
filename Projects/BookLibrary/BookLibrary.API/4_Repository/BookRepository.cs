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

    public async Task<Book> AddBook(Book book)
    {
        _libraryContext.Add(book);
        await _libraryContext.SaveChangesAsync();
        return book;
    }

    public async Task DeleteBookById(int id)
    {
        var book = await _libraryContext.Books.FindAsync(id);
        if (book != null)
        {
            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _libraryContext.Books
            .Select(b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublicationYear = b.PublicationYear,
                IsAvailable = b.IsAvailable,
                AuthorId = b.AuthorId,
                Author = b.AuthorId.HasValue ? _libraryContext.Authors.Where(a => a.Id == b.AuthorId).Select(a => a.Name).FirstOrDefault() : null
            })
            .ToListAsync();
    }

    public async Task<Book> GetBookById(int id)
    {
        var book = await _libraryContext.Books.FindAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {id} does not exist.");
        }
        return book;
    }

}