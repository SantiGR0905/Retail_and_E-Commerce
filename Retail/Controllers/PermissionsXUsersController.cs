using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsXUsersController : Controller
{
    private readonly IPermissionsXUsersService _permissionsXUsersService;
    public PermissionsXUsersController(IPermissionsXUsersService permissionsXUsersService)
    {
        _permissionsXUsersService = permissionsXUsersService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PermissionsXUsers>>> GetPermissionsXUsers()
    {
        var permissionxuser = await _permissionsXUsersService.GetPermissionsXUsers();
        return Ok(permissionxuser);
    }
    [HttpGet("{idpermissionxuser}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PermissionsXUsers>> GetPermissionsXUsersById(int idpermissionxuser)
    {
        var permissionxuser = await _permissionsXUsersService.GetPermissionsXUsersById(idpermissionxuser);
        if (permissionxuser == null)
            return NotFound();

        return Ok(permissionxuser);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePermissionsXUsers([FromBody] PermissionsXUsers permissionxuser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _permissionsXUsersService.CreatePermissionsXUsers(permissionxuser);
        return CreatedAtAction(nameof(GetPermissionsXUsersById), new { idpermissionxuser = permissionxuser.PermissionXUserId }, permissionxuser);
    }
    [HttpPut("{idpermissionxuser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePermissionsXUsers(int idpermissionxuser, [FromBody] PermissionsXUsers permissionxuser)
    {
        if (idpermissionxuser != permissionxuser.PermissionXUserId)
            return BadRequest();

        var existingPermissionsXUsers = await _permissionsXUsersService.GetPermissionsXUsersById(idpermissionxuser);
        if (existingPermissionsXUsers == null)
            return NotFound();

        await _permissionsXUsersService.UpdatePermissionsXUsers(permissionxuser);
        return NoContent();
    }

    [HttpDelete("{idpermissionxuser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeletePermissionsXUsers(int idpermissionxuser)
    {
        var permissionxuser = await _permissionsXUsersService.GetPermissionsXUsersById(idpermissionxuser);
        if (permissionxuser == null)
            return NotFound();

        await _permissionsXUsersService.SoftDeletePermissionsXUsers(idpermissionxuser);
        return NoContent();
    }
}