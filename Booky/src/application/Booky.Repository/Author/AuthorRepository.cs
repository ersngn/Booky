using Booky.Domain.Dtos.Author.Response;
using Booky.Infrastructure;

namespace Booky.Repository.Author;

public class AuthorRepository:IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Domain.Models.Author author)
    {
         _context.Authors.Add(author);
         _context.SaveChanges();
    }

    public AuthorWithBooksResponse GetAuthorWithBooks(int authorId)
    {
        var author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksResponse()
        {
            FullName = n.FullName,
            BookTitles = n.BookAuthors.Select(n => n.Book.Title).ToList()
        }).FirstOrDefault();

        return author;
    }
}