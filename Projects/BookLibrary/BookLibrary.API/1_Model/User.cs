using Microsoft.Data.SqlClient;

namespace BookLibrary.API.Data;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public required string Role { get; set; }
    public ICollection<Borrowing>? Borrowings { get; set; }
    
}