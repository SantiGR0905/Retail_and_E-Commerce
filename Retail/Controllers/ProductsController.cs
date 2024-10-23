using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IProductsService _productsService;
    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
    {
        var products = await _productsService.GetProduct();
        return Ok(products);
    }

    [HttpGet("{idProduct}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Products>> GetProductById(int idProduct)
    {
        var product = await _productsService.GetProductById(idProduct);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateProduct(string productName, string description, DateTime creationDate, int active, string model3D, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _productsService.CreateProduct(productName, description, creationDate, active, model3D, categoryId);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Product created successfully.");
    }

    [HttpPut("{idProduct}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(int idProduct, string productName, string description, DateTime creationDate, int active, string model3D, int categoryId)
    {
        var existingProducts = await _productsService.GetProductById(idProduct);
        if (existingProducts == null) return NotFound();


        try
        {
            await _productsService.UpdateProduct(idProduct, productName, description, creationDate, active, model3D, categoryId);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idProduct}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteProduct(int idProduct)
    {
        var products = await _productsService.GetProductById(idProduct);
        if (products == null)
            return NotFound();

        try
        {
            await _productsService.SoftDeleteProduct(idProduct);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}
