using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using Retail.Model.Dto;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesDetailsController : Controller
{
    private readonly ISalesDetailsService _salesDetailsService;
    public SalesDetailsController(ISalesDetailsService salesDetailsService)
    {
        _salesDetailsService = salesDetailsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SalesDetails>>> GetSalesDetails()
    {
        var salesDetails = await _salesDetailsService.GetSalesDetails();
        return Ok(salesDetails);
    }

    [HttpGet("{idSaleDetail}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalesDetails>> GetSaleDetailById(int idSaleDetail)
    {
        var salesDetails = await _salesDetailsService.GetSaleDetailById(idSaleDetail);
        if (salesDetails == null)
            return NotFound();

        return Ok(salesDetails);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateSaleDetail([FromBody] SalesDetailsDto saleDetail)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _salesDetailsService.CreateSaleDetail(saleDetail.Quantity, saleDetail.UnitPrice, saleDetail.SaleId, saleDetail.ProductId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Sale Detail created successfully.");
    }

    [HttpPut("{idSaleDetail}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSaleDetail(int idSaleDetail, [FromBody] SalesDetailsDto salesDetails)
    {
        var existingSalesDetails = await _salesDetailsService.GetSaleDetailById(idSaleDetail);
        if (existingSalesDetails == null) return NotFound();


        try
        {
            await _salesDetailsService.UpdateSaleDetail(idSaleDetail, salesDetails.Quantity, salesDetails.UnitPrice, salesDetails.SaleId, salesDetails.ProductId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idSaleDetail}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteSaleDetail(int idSaleDetail)
    {
        var salesDetails = await _salesDetailsService.GetSaleDetailById(idSaleDetail);
        if (salesDetails == null)
            return NotFound();

        try
        {
            await _salesDetailsService.SoftDeleteSaleDetail(idSaleDetail);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}
