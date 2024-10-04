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
    public async Task<ActionResult> CreatePermissions([FromBody] Permissions permissions)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _permissionsService.CreatePermissions(permissions);
        return CreatedAtAction(nameof(GetPermissionsById), new { permissionid = permissions.PermissionId }, permissions);
    }
    [HttpPut("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePermissions(int permissionid, [FromBody] Permissions permissions)
    {
        if (permissionid != permissions.PermissionId)
            return BadRequest();

        var existingPermissions = await _permissionsService.GetPermissionsById(permissionid);
        if (existingPermissions == null)
            return NotFound();

        await _permissionsService.UpdatePermissions(permissions);
        return NoContent();
    }

    [HttpDelete("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeletePermissions(int permissionid)
    {
        var permissions = await _permissionsService.GetPermissionsById(permissionid);
        if (permissions == null)
            return NotFound();

        await _permissionsService.SoftDeletePermissions(permissionid);
        return NoContent();
    }
}
