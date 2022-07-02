using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Dtos;
using WebAPI.Service.Services;

namespace WebAPI.Application.Controllers;

[Route("api/[controller]")]
public class OrderItemController : Controller
{
    private readonly IService<OrderItemDto> _orderItemService;

    public OrderItemController(IService<OrderItemDto> orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderItems()
    {
        var res = await _orderItemService.GetAsync();
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItem(int id)
    {
        var res = await _orderItemService.GetByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDto orderItem)
    {
        var res = await _orderItemService.InsertAsync(orderItem);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemDto orderItem)
    {
        var res = await _orderItemService.UpdateByIdAsync(id, orderItem);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var res = await _orderItemService.DeleteByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
}