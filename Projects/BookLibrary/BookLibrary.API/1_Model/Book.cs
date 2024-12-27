using System.ComponentModel.DataAnnotations;

namespace BookLibrary.API.Data;

public class Book
{
    public int Id { get; set;}
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public int? PublicationYear { get; set;}
    public bool IsAvailable { get; set; }
    public required int AuthorId { get; set; }
    public required Author Author { get; set; }

}
