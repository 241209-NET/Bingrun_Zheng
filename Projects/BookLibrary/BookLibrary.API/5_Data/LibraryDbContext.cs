using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext() { }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.User)
            .WithMany(u => u.Borrowings)
            .HasForeignKey(b => b.UserId);

    }
}