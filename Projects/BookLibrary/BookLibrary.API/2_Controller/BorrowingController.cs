using BookLibrary.API.Data;
using BookLibrary.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowingService _borrowingService;

    public BorrowingController(IBorrowingService borrowingService)
    {
        _borrowingService = borrowingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Borrowing>>> GetAllBorrowings()
    {
        return Ok(await _borrowingService.GetAllBorrowings());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Borrowing>> GetBorrowingById(int id)
    {
        var borrowing = await _borrowingService.GetBorrowingById(id);
        if (borrowing == null)
        {
            return NotFound();
        }
        return Ok(borrowing);
    }

    [HttpPost]
    public async Task<ActionResult<Borrowing>> AddBorrowing(Borrowing borrowing)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var addedBorrowing = await _borrowingService.AddBorrowing(borrowing);
            return CreatedAtAction(nameof(GetBorrowingById), new { id = addedBorrowing.Id }, addedBorrowing);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrowingById(int id){
        var borrowing = await _borrowingService.GetBorrowingById(id);
        if(borrowing == null){
            return NotFound();
        }
        await _borrowingService.DeleteBorrowingById(id);
        return Ok();
    }

    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id){
        var borrowing = await _borrowingService.GetBorrowingById(id);
        if(borrowing == null){
            return NotFound();
        }
        await _borrowingService.ReturnBook(id);
        return Ok();
    }

}