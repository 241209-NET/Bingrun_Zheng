using BookLibrary.API.Data;

namespace BookLibrary.API.Service;

public interface IBorrowingService
{
    Task<IEnumerable<Borrowing>> GetAllBorrowings();
    Task<Borrowing> GetBorrowingById(int id);
    Task<Borrowing> AddBorrowing(Borrowing borrowing);
    Task DeleteBorrowingById(int id);
    Task ReturnBook(int borrowingId);
}