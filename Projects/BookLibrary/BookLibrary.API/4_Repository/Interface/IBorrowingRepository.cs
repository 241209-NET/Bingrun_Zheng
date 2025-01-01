using BookLibrary.API.Data;

namespace BookLibrary.API.Repository;

public interface IBorrowingRepository
{
    Task<IEnumerable<Borrowing>> GetAllBorrowings();
    Task<Borrowing> GetBorrowingById(int id);
    Task<Borrowing> AddBorrowing(Borrowing borrowing);
    Task DeleteBorrowingById(int id);
    Task ReturnBook(int borrowingId);
}