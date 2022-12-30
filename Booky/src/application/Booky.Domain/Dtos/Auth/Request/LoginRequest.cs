using System.ComponentModel.DataAnnotations;
using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Auth.Request;

public class LoginRequest:IResponse
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}