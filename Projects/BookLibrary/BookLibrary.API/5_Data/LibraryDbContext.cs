using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(){}

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options){}

}