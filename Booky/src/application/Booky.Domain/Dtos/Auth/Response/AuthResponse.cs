using Booky.Domain.Dtos.Base;

namespace Booky.Domain.Dtos.Auth;

public class AuthResponse:IResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}