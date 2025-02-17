using Core.DTOs;
using Core.Interfaces.IServices;
using Core.Specifications.SpecParams;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class DeliveryMethodsController : ControllerBase
    {
        private readonly IDeliveryMethodService _deliveryMethodService;

        public DeliveryMethodsController(IDeliveryMethodService deliveryMethodService)
        {
            _deliveryMethodService = deliveryMethodService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<PaginatedResponse<DeliveryMethodDto>>> GetDeliveryMethods([FromQuery] DeliveryMethodSpecParams specParams)
        {
            var methods = await _deliveryMethodService.GetAllAsync(specParams);
            return Ok(methods);
        }

        [HttpGet("{id:int}"), AllowAnonymous]
        public async Task<ActionResult<DeliveryMethodDto>> GetDeliveryMethod(int id)
        {
            var method = await _deliveryMethodService.GetByIdAsync(id);
            if (method == null) return NotFound($"Delivery method with ID {id} not found.");
            return Ok(method);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryMethodDto>> CreateDeliveryMethod(DeliveryMethodDto methodDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdMethod = await _deliveryMethodService.CreateAsync(methodDto);
            return CreatedAtAction(nameof(GetDeliveryMethod), new { id = createdMethod.Id }, createdMethod);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateDeliveryMethod(int id, DeliveryMethodDto methodDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _deliveryMethodService.UpdateAsync(id, methodDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeliveryMethod(int id)
        {
            try
            {
                await _deliveryMethodService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

