using Booky.Domain.Dtos.Author.Response;

namespace Booky.Repository.Author;

public interface IAuthorRepository
{
    Task Create(Domain.Models.Author author);
    AuthorWithBooksResponse GetAuthorWithBooks(int authorId);
}