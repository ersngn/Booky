using Booky.Infrastructure;

namespace Booky.Repository.BookAuthor;

public class BookAuthorRepository:IBookAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public BookAuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Domain.Models.BookAuthor bookAuthor)
    {
        _context.BooksAuthors.Add(bookAuthor);
        _context.SaveChanges();
    }
}