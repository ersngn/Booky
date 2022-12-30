using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Author.Response;

public class AuthorResponse:IResponse
{
    public string FullName { get; set; }
}