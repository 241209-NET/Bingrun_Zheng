namespace BookLibrary.API.Data;

public class Borrowing
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required User User { get; set; }
    public required int BookId { get; set; }
    public required Book Book { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
}