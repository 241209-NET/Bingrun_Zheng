using BookLibrary.API.Data;
using BookLibrary.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService){
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks(){
        return Ok(await _bookService.GetAllBooks());
    }
}