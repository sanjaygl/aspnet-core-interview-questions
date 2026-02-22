using API.Models;
using API.Services.Order;
using API.Services.Order.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("{orderid}")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] long orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody] OrderCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid order data.");
            }

            OrderCreateDto orderCreateDto = new OrderCreateDto
            {
                OrderId = request.OrderId,
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount
            };

            var createdOrder = await _orderService.CreateOrderAsync(orderCreateDto);

            if (createdOrder == null)
            {
                return BadRequest("Failed to create order.");
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteAsync([FromQuery] long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid order ID.");
            }

            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] long id, [FromBody] OrderUpdateRequest request)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid order ID.");
            }

            if (request == null)
            {
                return BadRequest("Invalid order data.");
            }

            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            OrderUpdateDto orderUpdateDto = new OrderUpdateDto
            {
                OrderStatus = request.OrderStatus
            };

            await _orderService.UpdateOrderAsync(id, orderUpdateDto);

            return NoContent();
        }
    }
}