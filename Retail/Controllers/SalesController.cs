using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

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
    public async Task<ActionResult> CreateSales([FromBody] Sales sales)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _salesService.CreateSales(sales);
        return CreatedAtAction(nameof(GetSalesById), new { idsale = sales.SaleId }, sales);
    }
    [HttpPut("{idsale}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSales(int idsale, [FromBody] Sales sales)
    {
        if (idsale != sales.SaleId)
            return BadRequest();

        var existingSales = await _salesService.GetSalesById(idsale);
        if (existingSales == null)
            return NotFound();

        await _salesService.UpdateSales(sales);
        return NoContent();
    }

    [HttpDelete("{idsale}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteSales(int idsale)
    {
        var sales = await _salesService.GetSalesById(idsale);
        if (sales == null)
            return NotFound();

        await _salesService.SoftDeleteSales(idsale);
        return NoContent();
    }
}