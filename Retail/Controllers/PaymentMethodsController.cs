using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using Retail.Model.Dto;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]

public class PaymentMethodsController : ControllerBase
{
    private readonly IPaymentMethodsService _paymentMethodsService;
    public PaymentMethodsController(IPaymentMethodsService paymentMethodsService)
    {
        _paymentMethodsService = paymentMethodsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentMethods>>> GetPaymentMethod()
    {
        var paymentMethods = await _paymentMethodsService.GetPaymentMethod();
        return Ok(paymentMethods);
    }

    [HttpGet("{idPaymentMethod}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentMethods>> GetPaymentMethodById(int idPaymentMethod)
    {
        var paymentMethod = await _paymentMethodsService.GetPaymentMethodById(idPaymentMethod);
        if (paymentMethod == null)
            return NotFound();

        return Ok(paymentMethod);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreatePaymentMethod([FromBody] PaymentMethodsDto paymentMethod)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _paymentMethodsService.CreatePaymentMethod(paymentMethod.MethodName, paymentMethod.DescriptionMethod);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Payment Method created successfully.");
    }

    [HttpPut("{idPaymentMethod}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePaymentMethod(int idPaymentMethod, PaymentMethodsDto paymentMethod)
    {
        var existingPaymentMethod = await _paymentMethodsService.GetPaymentMethodById(idPaymentMethod);
        if (existingPaymentMethod == null) return NotFound();


        try
        {
            await _paymentMethodsService.UpdatePaymentMethod(paymentMethod.PaymentMethodId, paymentMethod.MethodName, paymentMethod.DescriptionMethod);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idPaymentMethod}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeletePaymentMethod(int idPaymentMethod)
    {
        var paymentMethod = await _paymentMethodsService.GetPaymentMethodById(idPaymentMethod);
        if (paymentMethod == null)
            return NotFound();

        try
        {
            await _paymentMethodsService.SoftDeletePaymentMethod(idPaymentMethod);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}