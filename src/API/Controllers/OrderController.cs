using API.Services.Orders;
using API.Services.Orders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized(new { Message = "User identity context missing." });
        }

        // Delegate all evaluation processing down to the business service layer
        var result = await _orderService.CreateOrderAsync(dto, username);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Message });
        }

        return CreatedAtAction(nameof(GetUserOrders), new { id = result.Data!.Id }, result.Data);
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetUserOrders()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username)) return Unauthorized();

        var orders = await _orderService.GetUserOrdersAsync(username);
        return Ok(orders);
    }
}
