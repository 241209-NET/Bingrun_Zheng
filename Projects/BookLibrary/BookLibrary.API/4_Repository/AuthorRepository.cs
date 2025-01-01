using BookLibrary.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookLibrary.API.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _libraryContext;

    public AuthorRepository(LibraryDbContext libraryDbContext)
    {
        _libraryContext = libraryDbContext;
    }

    public async Task<Author> AddAuthor(Author author)
    {
        _libraryContext.Add(author);
        await _libraryContext.SaveChangesAsync();
        return author;
    }

    public async Task DeleteAuthorById(int id)
    {
        var author = await _libraryContext.Authors.FindAsync(id);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with id {id} does not exist.");
        }
        _libraryContext.Authors.Remove(author);
        await _libraryContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _libraryContext.Authors.ToListAsync();
    }

    public async Task<Author> GetAuthorById(int id)
    {
        var author = await _libraryContext.Authors.FindAsync(id);
        if (author == null)
        {
            throw new KeyNotFoundException($"The author with id {id} do not exist.");
        }
        return author;
    }

    public async Task AddBookToAuthorById(int authorId, int bookId)
    {
        var author = await _libraryContext.Authors
            .FindAsync(authorId);

        if (author == null)
        {
            throw new KeyNotFoundException($"Author with id {authorId} does not exist.");
        }

        var book = await _libraryContext.Books.FindAsync(bookId);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {bookId} does not exist.");
        }

        if (author.Books == null)
        {
            author.Books = new List<string>();
        }

        if (!author.Books.Contains(book.Title))
        {
            author.Books.Add(book.Title);
            book.AuthorId = author.Id;
            book.Author = author.Name;
            await _libraryContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"This book is already owned by the author.");
        }
    }
}