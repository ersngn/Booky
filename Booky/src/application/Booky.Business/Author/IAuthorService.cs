using Booky.Domain.Dtos.Author.Request;
using Booky.Domain.Dtos.Author.Response;

namespace Booky.Business.Author;

public interface IAuthorService
{
     Task Create(CreateAuthorRequest request);
     AuthorWithBooksResponse GetAuthorWithBooks(int authorId);

}