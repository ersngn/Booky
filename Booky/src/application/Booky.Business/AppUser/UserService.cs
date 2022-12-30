using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Booky.Common.Constants;
using Booky.Domain.Dtos.Auth;
using Booky.Domain.Dtos.Auth.Request;
using Booky.Domain.Models;
using Booky.Repository.User;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Booky.Business.User;

public class UserService:IUserService
{
    private readonly UserManager<Domain.Models.User.User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<Domain.Models.User.User> userManager, IConfiguration configuration,IUserRepository userRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<RegisterDto> Register(RegisterRequest request)
    {
        var result = new RegisterDto();
        var userExists = await _userManager.FindByEmailAsync(request.Email);

        if(userExists != null)
        {
            result.Status = false;
            result.Messsage = $"User {request.Email} already exists";
            
            return result;
        }

        Domain.Models.User.User user = new Domain.Models.User.User()
        {
            Email = request.Email,
            UserName = request.UserName,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var registerResult = await _userManager.CreateAsync(user, request.Password);

        if (!registerResult.Succeeded)
        {
            result.Status = false;
            result.Messsage = $"User {request.Email} could not registered.";
        }

        switch (request.Role)
        {
            case "Admin":
                await _userManager.AddToRoleAsync(user.SecurityStamp, UserRoleConstant.Admin);
                break;
            case "Publisher":
                await _userManager.AddToRoleAsync(user.SecurityStamp, UserRoleConstant.Publisher);
                break;
            case "Author":
                await _userManager.AddToRoleAsync(user.SecurityStamp, UserRoleConstant.Author);
                break;
            default:
                await _userManager.AddToRoleAsync(user.SecurityStamp, UserRoleConstant.User);
                break;
        }
        
        result.Status = true;

        return result;
    }

    public async Task<AuthResponse> GenerateJwtToken(Domain.Models.User.User user)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //Add User Roles
        var userRoles = await _userManager.GetRolesAsync(user.SecurityStamp);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }


        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(10), // 5 - 10mins
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        var refreshToken = new RefreshToken()
        {
            JwtId = token.Id,
            IsRevoked = false,
            UserId = user.Id,
            DateAdded = DateTime.UtcNow,
            DateExpire = DateTime.UtcNow.AddMonths(6),
            Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
        };

        await _userRepository.CreateToken(refreshToken);

        var response = new AuthResponse()
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = token.ValidTo
        };
        
        return response;
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password)) return new AuthResponse();
            var token = await GenerateJwtToken(user);

        return token;

    }
}