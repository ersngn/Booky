using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Security;

public class ValidateHashDto : IDto
{
    public bool isValidated { get; set; }
}