using BookLibrary.API.Data;
using BookLibrary.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _userService.AddUser(user);
        return Ok(user);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        try
        {
            await _userService.DeleteUserById(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the user: {ex.Message}");
        }
    }

}