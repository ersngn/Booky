using Booky.Business.Book;
using Booky.Common.Constants;
using Booky.Domain.Dtos.Book.Request;
using Booky.Domain.Dtos.Book.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booky.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [Authorize(Roles = UserRoleConstant.Author)]
    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var allBooks = _bookService.Get();
        return Ok(allBooks);
    }
    
    [Authorize(Roles = UserRoleConstant.Admin)]
    [HttpGet("{id}")]
    public IActionResult GetBookById([FromRoute]int id)
    {
        var book = _bookService.GetById(id);
        return Ok(book);
    }
    
    [HttpPost]
    public IActionResult AddBook([FromBody]CreateBookRequest book)
    {
        _bookService.Create(book);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateBookById(int id, [FromBody] UpdateBookRequest book)
    {
        var updatedBook = _bookService.Update(id, book);
        return Ok(updatedBook);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteBookById(int id)
    {
        _bookService.Delete(id);
        return Ok();
    }
}