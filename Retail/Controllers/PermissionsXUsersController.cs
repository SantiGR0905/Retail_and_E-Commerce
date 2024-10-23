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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreatePermissionsXUsers(int userTypeId, int permissionId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _permissionsXUsersService.CreatePermissionsXUsers(userTypeId, permissionId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "PermissionXUser created successfully.");
    }
    [HttpPut("{idpermissionxuser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePermissionsXUsers(int idpermissionxuser, int userTypeId, int permissionId)
    {
        var existingPermissionsXUsers = await _permissionsXUsersService.GetPermissionsXUsersById(idpermissionxuser);
        if (existingPermissionsXUsers == null) return NotFound();


        try
        {
            await _permissionsXUsersService.UpdatePermissionsXUsers(idpermissionxuser, userTypeId, permissionId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idpermissionxuser}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeletePermissionsXUsers(int idpermissionxuser)
    {
        var permissionsxuser = await _permissionsXUsersService.GetPermissionsXUsersById(idpermissionxuser);
        if (permissionsxuser == null)
            return NotFound();

        try
        {
            await _permissionsXUsersService.SoftDeletePermissionsXUsers(idpermissionxuser);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
    [HttpGet("validate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ValidatePermission(int userTypeId, int permissionId)
    {
        bool hasPermission = await _permissionsXUsersService.HasPermissionAsync(userTypeId, permissionId);

        if (hasPermission)
        {
            return Ok(new { Message = "User has the required permission." });
        }

        return StatusCode(StatusCodes.Status403Forbidden, "User does not have the required permission.");
    }
}