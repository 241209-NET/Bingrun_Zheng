using BookLibrary.API.Data;
using BookLibrary.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
    {
        return Ok(await _authorService.GetAllAuthors());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        var author = await _authorService.GetAuthorById(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(Author author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _authorService.AddAuthor(author);
        return Ok(author);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorById(int id)
    {
        try
        {
            await _authorService.DeleteAuthorById(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the author.");
        }
    }

    [HttpPost("{authorId}/books/{bookId}")]
    public async Task<IActionResult> AddBookToAuthorById(int authorId, int bookId)
    {
        try
        {
            await _authorService.AddBookToAuthorById(authorId, bookId);
            return Ok(new { message = $"Book with id {bookId} has been added to author with id {authorId} successfully", });
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}