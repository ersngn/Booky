using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Security;

public class HashedTextDto : IDto
{
    public string HashedText { get; set; }
}