using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoriesController : ControllerBase
{
    private readonly IInventoriesService _inventoriesService;
    public InventoriesController(IInventoriesService inventoriesService)
    {
        _inventoriesService = inventoriesService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Inventories>>> GetInventories()
    {
        var inventories = await _inventoriesService.GetInventory();
        return Ok(inventories);
    }

    [HttpGet("{idInventory}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Inventories>> GetInventoryById(int idInventory)
    {
        var inventory = await _inventoriesService.GetInventoryById(idInventory);
        if (inventory == null)
            return NotFound();

        return Ok(inventory);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateInventory([FromBody] Inventories inventory)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _inventoriesService.CreateInventory(inventory);
        return CreatedAtAction(nameof(GetInventoryById), new { idInventory = inventory.InventoryId }, inventory);
    }

    [HttpPut("{idInventory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateInventory(int idInventory, [FromBody] Inventories inventory)
    {
        if (idInventory != inventory.InventoryId)
            return BadRequest();

        var existingInventory = await _inventoriesService.GetInventoryById(idInventory);
        if (existingInventory == null)
            return NotFound();

        await _inventoriesService.UpdateInventory(inventory);
        return NoContent();
    }

    [HttpDelete("{idInventory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteInventory(int idInventory)
    {
        var inventory = await _inventoriesService.GetInventoryById(idInventory);
        if (inventory == null)
            return NotFound();

        await _inventoriesService.SoftDeleteInventory(idInventory);
        return NoContent();
    }
}