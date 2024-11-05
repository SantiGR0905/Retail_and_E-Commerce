using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using System;
using Retail.Model.Dto;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemsController : Controller
{
    private readonly ICartItemsService _cartItemsService;
    public CartItemsController(ICartItemsService cartItemsService)
    {
        _cartItemsService = cartItemsService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CartItems>>> GetCartItems()
    {
        var cartItems = await _cartItemsService.GetCartItems();
        return Ok(cartItems);
    }
    [HttpGet("{idcartItem}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartItems>> GetCartItemById(int idcartItem)
    {
        var cartItems = await _cartItemsService.GetCartItemById(idcartItem);
        if (cartItems == null)
            return NotFound();

        return Ok(cartItems);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateCartItem([FromBody] CartItemsDto cartItem)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _cartItemsService.CreateCartItem(cartItem.Quantity, cartItem.CartId, cartItem.ProductId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Cart Item created successfully.");
    }
    [HttpPut("{idcartItem}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCartItem(int idcartItem, [FromBody] CartItemsDto cartItem)
    {
        var existingCartItems = await _cartItemsService.GetCartItemById(idcartItem);
        if (existingCartItems == null) return NotFound();


        try
        {
            await _cartItemsService.UpdateCartItem(idcartItem, cartItem.Quantity, cartItem.CartId, cartItem.ProductId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idcartItem}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteCartItem(int idcartItem)
    {
        var cartItems = await _cartItemsService.GetCartItemById(idcartItem);
        if (cartItems == null)
            return NotFound();

        try
        {
            await _cartItemsService.SoftDeleteCartItem(idcartItem);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}