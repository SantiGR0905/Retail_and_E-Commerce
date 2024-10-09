using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
    {
        var users = await _usersService.GetUsers();
        return Ok(users);
    }
    [HttpGet("{idUser}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Users>> GetUsersById(int idUser)
    {
        var users = await _usersService.GetUsersById(idUser);
        if (users == null)
            return NotFound();

        return Ok(users);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateSales([FromBody] Users user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _usersService.CreateUsers(user);
        return CreatedAtAction(nameof(GetUsersById), new { idUser = user.UserId }, user);
    }
    [HttpPut("{idUser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUsers(int idUser, [FromBody] Users user)
    {
        if (idUser != user.UserId)
            return BadRequest();

        var existingUsers = await _usersService.GetUsersById(idUser);
        if (existingUsers == null)
            return NotFound();

        await _usersService.UpdateUsers(user);
        return NoContent();
    }

    [HttpDelete("{idUser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteUsers(int idUser)
    {
        var users = await _usersService.GetUsersById(idUser);
        if (users == null)
            return NotFound();

        await _usersService.SoftDeleteUsers(idUser);
        return NoContent();
    }
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult> ValidateUser(string email, string password)
    {
        if (email == null || password == null) return BadRequest(ModelState);

        // Validate the user
        try
        {
            var isValid = await _usersService.ValidateUserAsync(email, password);
            if (isValid)
            {
                // Handle successful login  
                return Ok(new { Message = "Login successful" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }

        // Handle failed login
        return Unauthorized(new { Message = "Invalid Password" });
    }


}