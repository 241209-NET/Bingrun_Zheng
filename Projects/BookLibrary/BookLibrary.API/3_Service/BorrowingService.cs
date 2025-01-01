using BookLibrary.API.Data;
using BookLibrary.API.Repository;

namespace BookLibrary.API.Service;

public class BorrowingService : IBorrowingService
{
    private readonly IBorrowingRepository _borrowingRepository;

    public BorrowingService(IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<Borrowing> AddBorrowing(Borrowing borrowing)
    {
        return await _borrowingRepository.AddBorrowing(borrowing);
    }

    public async Task DeleteBorrowingById(int id)
    {
        await _borrowingRepository.DeleteBorrowingById(id);
    }

    public async Task<IEnumerable<Borrowing>> GetAllBorrowings()
    {
        return await _borrowingRepository.GetAllBorrowings();
    }

    public async Task<Borrowing> GetBorrowingById(int id)
    {
        return await _borrowingRepository.GetBorrowingById(id);
    }

    public async Task ReturnBook(int borrowingId)
    {
        await _borrowingRepository.ReturnBook(borrowingId);
    }
}