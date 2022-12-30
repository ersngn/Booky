using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Security;

public class EncyrptTextDto : IDto
{
    public string? EncryptedText { get; set; }
}