using API.RequestHelpers;
using Core.DTOs;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Cache(10000)]
        [HttpGet,AllowAnonymous]
        public async Task<ActionResult<PaginatedResponse<ProductDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var products = await _productService.GetAllAsync(specParams);
            return Ok(products);
        }

        [Cache(10000)]
        [HttpGet("{id:int}"),AllowAnonymous]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound($"Product with ID {id} not found.");
            return Ok(product);
        }

        [InvalidateCache("/api/products")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);    
            var createdProduct = await _productService.CreateAsync(createProductDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [InvalidateCache("/api/products")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _productService.UpdateAsync(id, productDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [InvalidateCache("/api/products")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }           
        }
    }
}
