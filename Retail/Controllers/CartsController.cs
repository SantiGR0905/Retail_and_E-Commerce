using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using System;
using Retail.Model.Dto;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : Controller
{
    private readonly ICartsService _cartsService;
    public CartsController(ICartsService cartsService)
    {
        _cartsService = cartsService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Carts>>> GetCarts()
    {
        var carts = await _cartsService.GetCarts();
        return Ok(carts);
    }
    [HttpGet("{idcart}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Carts>> GetCartById(int idcart)
    {
        var carts = await _cartsService.GetCartById(idcart);
        if (carts == null)
            return NotFound();

        return Ok(carts);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateCart([FromBody] CartsDto cart)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _cartsService.CreateCart(cart.IsActive, cart.UserId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Cart created successfully.");
    }
    [HttpPut("{idcart}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCart(int idcart, [FromBody] CartsDto cart)
    {
        var existingCarts = await _cartsService.GetCartById(idcart);
        if (existingCarts == null) return NotFound();


        try
        {
            await _cartsService.UpdateCart(idcart, existingCarts.Created, cart.IsActive, cart.UserId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idcart}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteCarts(int idcart)
    {
        var carts = await _cartsService.GetCartById(idcart);
        if (carts == null)
            return NotFound();

        try
        {
            await _cartsService.SoftDeleteCart(idcart);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}