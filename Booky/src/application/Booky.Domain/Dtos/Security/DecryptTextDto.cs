using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Security;

public class DecryptTextDto : IDto
{
    public string? DecryptedText { get; set; }
}