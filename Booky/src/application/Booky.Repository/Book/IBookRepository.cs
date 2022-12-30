using Booky.Domain.Dtos.Book.Request;
using Booky.Domain.Dtos.Book.Response;

namespace Booky.Repository.Book;

public interface IBookRepository
{ 
    List<Domain.Models.Book> Get();
    BookWithAuthorsResponse GetById(int id);
    void Create(Domain.Models.Book book);
    Domain.Models.Book Update(int bookId, UpdateBookRequest book);
    void Delete(int bookId);
}