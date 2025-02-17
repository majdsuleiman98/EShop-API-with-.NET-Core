using API.RequestHelpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.IServices;
using Core.Specifications;
using Core.Specifications.SpecParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Cache(10000)]
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<PaginatedResponse<CategoryDto>>> GetCategories([FromQuery] CategorySpecParams specParams)
        {
            var categories = await _categoryService.GetAllAsync(specParams);
            return Ok(categories);
        }

        [Cache(10000)]
        [HttpGet("{id:int}"), AllowAnonymous]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound($"Category with ID {id} not found.");
            return Ok(category);
        }

        [InvalidateCache("/api/categories")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdcategory = await _categoryService.CreateAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdcategory.Id }, createdcategory);
        }

        [InvalidateCache("/api/categories")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _categoryService.UpdateAsync(id, categoryDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            
        }

        [InvalidateCache("/api/categories")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }           
        }
    }
}
