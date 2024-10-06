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
}