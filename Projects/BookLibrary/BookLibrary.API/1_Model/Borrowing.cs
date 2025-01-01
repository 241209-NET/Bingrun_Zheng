namespace BookLibrary.API.Data;

public class Borrowing
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public User? User { get; set; }
    public required int BookId { get; set; }
    public Book? Book { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
}