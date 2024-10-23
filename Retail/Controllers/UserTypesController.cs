using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTypesController : Controller
{
    private readonly IUserTypesService _userTypesService;
    public UserTypesController(IUserTypesService userTypesService)
    {
        _userTypesService = userTypesService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserTypes>>> GetUserTypes()
    {
        var usertypes = await _userTypesService.GetUserTypes();
        return Ok(usertypes);
    }
    [HttpGet("{idusertype}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserTypes>> GetUserTypesById(int idusertype)
    {
        var usertypes = await _userTypesService.GetUserTypesById(idusertype);
        if (usertypes == null)
            return NotFound();

        return Ok(usertypes);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateUserTypes(string userTypes)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _userTypesService.CreateUserTypes(userTypes);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "UserType created successfully.");

    }
    [HttpPut("{idusertype}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUserTypes(int idusertype, string userType)
    {
        var existingUserType = await _userTypesService.GetUserTypesById(idusertype);
        if (existingUserType == null) return NotFound();


        try
        {
            await _userTypesService.UpdateUserTypes(idusertype, userType);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idusertype}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteUserTypes(int idusertype)
    {
        var usertypes = await _userTypesService.GetUserTypesById(idusertype);
        if (usertypes == null)
            return NotFound();

        try
        {
            await _userTypesService.SoftDeleteUserTypes(idusertype);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }

    }
}