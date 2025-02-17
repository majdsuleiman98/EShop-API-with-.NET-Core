using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;
using Core.Specifications.SpecParams;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController(IAdminOrderService adminOrderService) : ControllerBase
    {
        [HttpGet("orders")]
        public async Task<ActionResult<PaginatedResponse<OrderDto>>> GetOrders([FromQuery]OrderSpecParams specParams)
        {
            var orders = await adminOrderService.GetAllAsync(specParams);
            return Ok(orders);
        }

        [HttpGet("orders/{id:int}")]
        public async Task<ActionResult<OrderDto?>> GetOrder(int Id)
        {
            var order = await adminOrderService.GetByIdAsync(Id);
            if (order == null) return NotFound($"Order with ID {Id} not found.");
            return Ok(order);
        }

    }
}
