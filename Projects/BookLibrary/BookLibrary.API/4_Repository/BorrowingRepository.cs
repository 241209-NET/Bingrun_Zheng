using BookLibrary.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Repository;

public class BorrowingRepository : IBorrowingRepository
{
    private readonly LibraryDbContext _libraryContext;

    public BorrowingRepository(LibraryDbContext libraryDbContext)
    {
        _libraryContext = libraryDbContext;
    }

    public async Task<Borrowing> AddBorrowing(Borrowing borrowing)
    {
        borrowing.BorrowDate = DateTime.Now;
        borrowing.DueDate = borrowing.BorrowDate.AddDays(7);

        var book = await _libraryContext.Books.FindAsync(borrowing.BookId);
        if (book == null || !book.IsAvailable)
        {
            throw new InvalidOperationException("The book you are borrowing is not available.");
        }

        var user = await _libraryContext.Users.FindAsync(borrowing.UserId);
        if (user == null)
        {
            throw new InvalidOperationException("The user does not exist.");
        }
        if (user.Borrowings == null)
        {
            user.Borrowings = new List<Borrowing>();
        }
        user.Borrowings.Add(borrowing);
        book.IsAvailable = false;
        _libraryContext.Add(borrowing);
        await _libraryContext.SaveChangesAsync();
        return borrowing;
    }

    public async Task DeleteBorrowingById(int id)
    {
        var borrowing = await _libraryContext.Borrowings.FindAsync(id);
        if (borrowing != null)
        {
            _libraryContext.Remove(borrowing);
            var book = await _libraryContext.Books.FindAsync(borrowing.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }
            await _libraryContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Borrowing>> GetAllBorrowings()
    {
        return await _libraryContext.Borrowings
            .Include(b => b.User)
            .Include(b => b.Book)
            .ToListAsync();
    }

    public async Task<Borrowing> GetBorrowingById(int id)
    {
        var borrowing = await _libraryContext.Borrowings
            .Include(b => b.User)
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (borrowing == null)
        {
            throw new KeyNotFoundException($"Borrowing with id {id} does not exist");
        }
        return borrowing;
    }

    public async Task ReturnBook(int borrowingId)
    {
        var borrowing = await _libraryContext.Borrowings
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == borrowingId);
        if (borrowing != null)
        {
            borrowing.ReturnDate = DateTime.Now;
            if (borrowing.Book != null)
            {
                borrowing.Book.IsAvailable = true;
            }
            await _libraryContext.SaveChangesAsync();
        }
    }
}