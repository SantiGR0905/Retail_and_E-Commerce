using Microsoft.AspNetCore.Mvc;
using Retail.Model;
using Retail.Services;
using Retail.Model.Dto;

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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateCategory([FromBody] CategoriesDto category)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _categoriesService.CreateCategory(category.CategoryName ,category.CategoryDescription);
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message); ;
        }


        return StatusCode(StatusCodes.Status201Created, "Category created successfully.");
    }

    [HttpPut("{idCategory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCategory(int idCategory, CategoriesDto category)
    {
        var existingCategory = await _categoriesService.GetCategoryById(idCategory);
        if (existingCategory == null) return NotFound();


        try
        {
            await _categoriesService.UpdateCategory(idCategory, category.CategoryName, category.CategoryDescription);
            return StatusCode(StatusCodes.Status200OK, ("Updated Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e.Message);
        }
    }

    [HttpDelete("{idCategory}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDeleteCategory(int idCategory)
    {
        var categories = await _categoriesService.GetCategoryById(idCategory);
        if (categories == null)
            return NotFound();

        try
        {
            await _categoriesService.SoftDeleteCategory(idCategory);
            return StatusCode(StatusCodes.Status200OK, ("Deleted Successfully"));
        }
        catch (Exception e)
        {
            return StatusCode(404, e?.Message);
        }
    }
}