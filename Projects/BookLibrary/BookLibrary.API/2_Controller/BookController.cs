using BookLibrary.API.Data;
using BookLibrary.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
        return Ok(await _bookService.GetAllBooks());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id){
        var book = await _bookService.GetBookById(id);
        if(book == null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        await _bookService.AddBook(book);
        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookById(int id){
        var book = await _bookService.GetBookById(id);
        if(book == null){
            return NotFound();
        }
        await _bookService.DeleteBookById(id);

        return Ok(book);
    }
}
