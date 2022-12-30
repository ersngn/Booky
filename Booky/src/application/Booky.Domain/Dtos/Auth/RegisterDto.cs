using Microsoft.AspNetCore.Identity;

namespace Booky.Domain.Dtos.Auth;

public class RegisterDto
{
    public bool Status { get; set; }
    public string Messsage { get; set; }
}