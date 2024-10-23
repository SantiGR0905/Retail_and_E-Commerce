using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using System;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : Controller
{
    private readonly ISalesService _salesService;
    public SalesController(ISalesService salesService)
    {
        _salesService = salesService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Sales>>> GetSales()
    {
        var sales = await _salesService.GetSales();
        return Ok(sales);
    }
    [HttpGet("{idsale}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Sales>> GetSalesById(int idsale)
    {
        var sales = await _salesService.GetSalesById(idsale);
        if (sales == null)
            return NotFound();

        return Ok(sales);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateSales(DateTime saleDate, int stateSale, string direction, int userId, int productId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _salesService.CreateSales(saleDate, stateSale, direction, userId, productId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Sales created successfully.");
    }
    [HttpPut("{idsale}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSales(int idsale,DateTime saleDate, int stateSale, string direction, int userId, int productId)
    {
        var existingSales = await _salesService.GetSalesById(idsale);
        if (existingSales == null) return NotFound();


        try
        {
            await _salesService.UpdateSales(idsale, saleDate, stateSale, direction, userId, productId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idsale}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteSales(int idsale)
    {
        var sales = await _salesService.GetSalesById(idsale);
        if (sales == null)
            return NotFound();

        try
        {
            await _salesService.SoftDeleteSales(idsale);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}