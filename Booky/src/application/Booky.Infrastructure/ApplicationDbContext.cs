using Booky.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booky.Infrastructure;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }  
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasOne(b => b.Book)
            .WithMany(ba => ba.BookAuthors)
            .HasForeignKey(bi => bi.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(b => b.Author)
            .WithMany(ba => ba.BookAuthors)
            .HasForeignKey(bi => bi.AuthorId);

        modelBuilder.Entity<Log>().HasKey(n => n.Id);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BooksAuthors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}