using Booky.Domain.Dtos.Book.Request;
using Booky.Domain.Dtos.Book.Response;

namespace Booky.Business.Book;

public interface IBookService
{ 
    List<Domain.Models.Book> Get();
    BookWithAuthorsResponse GetById(int id);
    void Create(CreateBookRequest book);
    Domain.Models.Book Update(int bookId, UpdateBookRequest book);
    void Delete(int bookId);
}