using API.Extentions;
using Core.DTOs;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Specifications;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var Email = User.GetEmail();
                var order = await orderService.CreateOrder(createOrderDto, Email);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your order");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var Email = User.GetEmail();
            var orders = await orderService.GetOrdersOfUser(Email);
            return Ok(orders);
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
        {
            var Email = User.GetEmail();
            var order = await orderService.GetOrderOfUser(Email, orderId);
            if (order == null) return NotFound($"Order with ID {orderId} not found.");
            return Ok(order);
        }
    }
}
