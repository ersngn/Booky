using Booky.Domain.Dtos.Book.Request;
using Booky.Domain.Dtos.Book.Response;
using Booky.Infrastructure;

namespace Booky.Repository.Book;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Domain.Models.Book> Get()
    {
        return _context.Books.ToList();
    }

    public BookWithAuthorsResponse GetById(int id)
    {
        var bookWithAuthors = _context.Books.Where(n => n.Id == id).Select(book => new BookWithAuthorsResponse()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.BookAuthors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return bookWithAuthors;
    }

    public void Create(Domain.Models.Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public Domain.Models.Book Update(int bookId, UpdateBookRequest book)
    {
        var bookEntity = _context.Books.FirstOrDefault(n => n.Id == bookId);
        if(bookEntity != null)
        {
            bookEntity.Title = book.Title;
            bookEntity.Description = book.Description;
            bookEntity.IsRead = book.IsRead;
            bookEntity.DateRead = book.IsRead ? book.DateRead.Value : null;
            bookEntity.Rate = book.IsRead ? book.Rate.Value : null;
            bookEntity.Genre = book.Genre;
            bookEntity.CoverUrl = book.CoverUrl;

            _context.SaveChanges();
        }

        return bookEntity;
    }

    public void Delete(int bookId)
    {
        var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
        if(_book != null)
        {
            _context.Books.Remove(_book);
            _context.SaveChanges();
        }
    }
}