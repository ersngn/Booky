using Booky.Domain.Dtos.Book.Request;
using Booky.Domain.Dtos.Book.Response;
using Booky.Domain.Models;
using Booky.Repository.Book;
using Booky.Repository.BookAuthor;

namespace Booky.Business.Book;

public class BookService:IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookAuthorRepository _bookAuthorRepository;

    public BookService(IBookRepository bookRepository,IBookAuthorRepository bookAuthorRepository)
    {
        _bookRepository = bookRepository;
        _bookAuthorRepository = bookAuthorRepository;
    }

    public List<Domain.Models.Book> Get()
    {
        return _bookRepository.Get();
    }

    public BookWithAuthorsResponse GetById(int id)
    {
        return _bookRepository.GetById(id);
    }

    public void Create(CreateBookRequest book)
    {
        var bookEntity = new Domain.Models.Book()
        {
            Title = book.Title,
            Description = book.Description,
            IsRead = book.IsRead,
            DateRead = book.IsRead ? book.DateRead.Value : null,
            Rate = book.IsRead ? book.Rate.Value : null,
            Genre = book.Genre,
            CoverUrl = book.CoverUrl,
            DateAdded = DateTime.Now,
            PublisherId = book.PublisherId
        };
        
        _bookRepository.Create(bookEntity);
        
        foreach (var id in book.AuthorIds)
        {
            var bookAuthor = new BookAuthor()
            {
                BookId = bookEntity.Id,
                AuthorId = id
            };
            
            _bookAuthorRepository.Create(bookAuthor);
        }
        
    }

    public Domain.Models.Book Update(int bookId, UpdateBookRequest book)
    {
        return _bookRepository.Update(bookId, book);
    }

    public void Delete(int bookId)
    {
        _bookRepository.Delete(bookId);
    }
}