using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsController : Controller
{
    private readonly IPermissionsService _permissionsService;
    public PermissionsController(IPermissionsService permissionsService)
    {
        _permissionsService = permissionsService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Permissions>>> GetPermissions()
    {
        var permissions = await _permissionsService.GetPermissions();
        return Ok(permissions);
    }
    [HttpGet("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Permissions>> GetPermissionsById(int permissionid)
    {
        var permissions = await _permissionsService.GetPermissionsById(permissionid);
        if (permissions == null)
            return NotFound();

        return Ok(permissions);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreatePermissions(string permission)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _permissionsService.CreatePermissions(permission);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }
        return StatusCode(StatusCodes.Status201Created, "Permission created successfully.");
    }
    [HttpPut("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<IActionResult> UpdatePermissions(int permissionid, string permission)
    {
        var existingPermissions = await _permissionsService.GetPermissionsById(permissionid);
        if (existingPermissions == null) return NotFound();

        try
        {
            await _permissionsService.UpdatePermissions(permissionid, permission);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }


    [HttpDelete("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeletePermissions(int permissionid)
    {
        var permissions = await _permissionsService.GetPermissionsById(permissionid);
        if (permissions == null)
            return NotFound();

        try
        {
            await _permissionsService.SoftDeletePermissions(permissionid);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}

