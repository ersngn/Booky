using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Author.Response;

public class AuthorWithBooksResponse:IResponse
{
    public string FullName { get; set; }
    public List<string> BookTitles { get; set; }
}