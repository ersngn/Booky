using Booky.Business.User;
using Booky.Domain.Dtos.Auth.Request;
using Booky.Domain.Models;
using Booky.Domain.Models.User;
using Booky.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Booky.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please, provide all required fields");
        }

        var result = await _userService.Register(request);
        if (!result.Status)
        {
            return BadRequest(result.Messsage);
        }
        return Created(nameof(Register), result.Messsage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please, provide all required fields");
        }

        var token = await _userService.Login(request);
        if (token.Token.IsNullOrEmpty())
        {
            return Ok(token);
        }

        return Unauthorized();
    }
    
    
}