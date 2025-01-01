using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext() { }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public required DbSet<Book> Books { get; set; }
    public required DbSet<User> Users { get; set; }
    public required DbSet<Borrowing> Borrowings { get; set; }
    public required DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.User)
            .WithMany(u => u.Borrowings)
            .HasForeignKey(b => b.UserId);
    }
}