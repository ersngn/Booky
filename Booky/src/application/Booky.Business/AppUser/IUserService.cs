using Booky.Domain.Dtos.Auth;
using Booky.Domain.Dtos.Auth.Request;
using Microsoft.AspNet.Identity;

namespace Booky.Business.User;

public interface IUserService
{
    Task<RegisterDto> Register(RegisterRequest request);
    Task<AuthResponse> GenerateJwtToken(Domain.Models.User.User user);

    Task<AuthResponse> Login(LoginRequest request);
}