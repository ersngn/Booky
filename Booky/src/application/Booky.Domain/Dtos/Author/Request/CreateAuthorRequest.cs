using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Author.Request;

public class CreateAuthorRequest:IRequest
{
    public string FullName { get; set; }
}