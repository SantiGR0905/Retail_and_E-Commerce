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
    public async Task<ActionResult> CreateProduct([FromBody] Products product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _productsService.CreateProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { idProduct = product.ProductId }, product);
    }

    [HttpPut("{idProduct}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int idProduct, [FromBody] Products product)
    {
        if (idProduct != product.ProductId)
            return BadRequest();

        var existingProduct = await _productsService.GetProductById(idProduct);
        if (existingProduct == null)
            return NotFound();

        await _productsService.UpdateProduct(product);
        return NoContent();
    }

    [HttpDelete("{idProduct}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteProduct(int idProduct)
    {
        var product = await _productsService.GetProductById(idProduct);
        if (product == null)
            return NotFound();

        await _productsService.SoftDeleteProduct(idProduct);
        return NoContent();
    }
}
