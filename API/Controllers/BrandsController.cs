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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService) 
        {
            _brandService = brandService;
        }

        [Cache(10000)]
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<PaginatedResponse<BrandDto>>> GetBrands([FromQuery]BrandSpecParams specParams)
        {
            var brands = await _brandService.GetAllAsync(specParams);
            return Ok(brands);
        }

        [Cache(10000)]
        [HttpGet("{id:int}"), AllowAnonymous]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound($"Brand with ID {id} not found.");
            return Ok(brand);
        }

        [InvalidateCache("/api/brands")]
        [HttpPost]
        public async Task<ActionResult<BrandDto>> CreateBrand(CreateBrandDto createBrandDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdbrand = await _brandService.CreateAsync(createBrandDto);
            return CreatedAtAction(nameof(GetBrand), new { id = createdbrand.Id }, createdbrand);
        }

        [InvalidateCache("/api/brands")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateBrand(int id, BrandDto brandDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _brandService.UpdateAsync(id, brandDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [InvalidateCache("/api/brands")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            try
            {
                await _brandService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
