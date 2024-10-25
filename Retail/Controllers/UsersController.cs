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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateUsers(string firstName, string lastName, string email, string password, DateTime date, int userTypeId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _usersService.CreateUsers(firstName, lastName, email, password, date, userTypeId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "User created successfully.");
    }
    [HttpPut("{idUser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUsers(int idUser, string firstName, string lastName, string email, string password, DateTime date, int userTypeId)
    {
        var existingUser = await _usersService.GetUsersById(idUser);
        if (existingUser == null) return NotFound();

        try
        {
            await _usersService.UpdateUsers(idUser, firstName, lastName, email, password, date, userTypeId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idUser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteUsers(int idUser)
    {
        var users = await _usersService.GetUsersById(idUser);
        if (users == null)
            return NotFound();

        try
        {
            await _usersService.SoftDeleteUsers(idUser);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]


    public async Task<ActionResult> ValidateUser([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest.Email == null || loginRequest.Password == null)
            return BadRequest(ModelState);

        try
        {
            var isValid = await _usersService.ValidateUserAsync(loginRequest.Email, loginRequest.Password);
            if (isValid)
            {
                return Ok(new { Message = "Login successful" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }

        return Unauthorized(new { Message = "Invalid Password" });
    }


}