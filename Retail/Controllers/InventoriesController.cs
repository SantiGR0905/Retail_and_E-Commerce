using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using Retail.Model.Dto;

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
    public async Task<ActionResult> CreateInventory([FromBody] InventoriesDto inventory)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _inventoriesService.CreateInventory(inventory.Amount, inventory.LastUpdate, inventory.ProductId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Inventory created successfully.");
    }

    [HttpPut("{idInventory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateInventory(int idInventory, [FromBody] InventoriesDto inventory)
    {
        var existingInventory = await _inventoriesService.GetInventoryById(idInventory);
        if (existingInventory == null) return NotFound();


        try
        {
            await _inventoriesService.UpdateInventory(idInventory, inventory.Amount, inventory.LastUpdate, inventory.ProductId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idInventory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteInventory(int idInventory)
    {
        var inventories = await _inventoriesService.GetInventoryById(idInventory);
        if (inventories == null)
            return NotFound();

        try
        {
            await _inventoriesService.SoftDeleteInventory(idInventory);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}