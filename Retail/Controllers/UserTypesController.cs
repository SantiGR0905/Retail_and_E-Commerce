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
    public async Task<ActionResult> CreateUserTypes([FromBody] UserTypes usertypes)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _userTypesService.CreateUserTypes(usertypes);
        return CreatedAtAction(nameof(GetUserTypesById), new { idusertype = usertypes.UserTypeId }, usertypes);
    }
    [HttpPut("{idusertype}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserTypes(int idusertype, [FromBody] UserTypes usertypes)
    {
        if (idusertype != usertypes.UserTypeId)
            return BadRequest();

        var existingUserTypes = await _userTypesService.GetUserTypesById(idusertype);
        if (existingUserTypes == null)
            return NotFound();

        await _userTypesService.UpdateUserTypes(usertypes);
        return NoContent();
    }

    [HttpDelete("{idusertype}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteUserTypes(int idusertype)
    {
        var usertypes = await _userTypesService.GetUserTypesById(idusertype);
        if (usertypes == null)
            return NotFound();

        await _userTypesService.SoftDeleteUserTypes(idusertype);
        return NoContent();
    }
}