using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;

namespace Retail.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
    {
        var categories = await _categoriesService.GetCategory();
        return Ok(categories);
    }

    [HttpGet("{idCategory}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Categories>> GetCategoryById(int idCategory)
    {
        var category = await _categoriesService.GetCategoryById(idCategory);
        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateCategory([FromBody] Categories category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _categoriesService.CreateCategory(category);
        return CreatedAtAction(nameof(GetCategoryById), new { idCategory = category.CategoryId }, category);
    }

    [HttpPut("{idCategory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(int idCategory, [FromBody] Categories category)
    {
        if (idCategory != category.CategoryId)
            return BadRequest();

        var existingCategory = await _categoriesService.GetCategoryById(idCategory);
        if (existingCategory == null)
            return NotFound();

        await _categoriesService.UpdateCategory(category);
        return NoContent();
    }

    [HttpDelete("{idCategory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteCategory(int idCategory)
    {
        var category = await _categoriesService.GetCategoryById(idCategory);
        if (category == null)
            return NotFound();

        await _categoriesService.SoftDeleteCategory(idCategory);
        return NoContent();
    }
}