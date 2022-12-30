using Booky.Domain.Dtos.Author.Request;
using Booky.Domain.Dtos.Author.Response;
using Booky.Repository;
using Booky.Repository.Author;

namespace Booky.Business.Author;

public class AuthorService:IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task Create(CreateAuthorRequest request)
    {
        var author = new Domain.Models.Author()
        {
            FullName = request.FullName
        };

        await _authorRepository.Create(author);
    }

    public AuthorWithBooksResponse GetAuthorWithBooks(int authorId)
    {
        return _authorRepository.GetAuthorWithBooks(authorId);
    }
}