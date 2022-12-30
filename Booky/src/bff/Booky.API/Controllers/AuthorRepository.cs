using Booky.Business.Author;
using Booky.Common.Constants;
using Booky.Domain.Dtos.Author.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booky.API.Controllers;

[Authorize(Roles = UserRoleConstant.Author)]
[Route("api/[controller]")]
[ApiController]
public class AuthorRepository : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorRepository(IAuthorService authorService)
    {
        _authorService = authorService;
    }
    
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateAuthorRequest author)
    {
        _authorService.Create(author);
        return Ok();
    }
    
    [HttpGet("get-author-with-books-by-id/{id}")]
    public IActionResult GetAuthorWithBooks(int id)
    {
        var response = _authorService.GetAuthorWithBooks(id);
        return Ok(response);
    }

}