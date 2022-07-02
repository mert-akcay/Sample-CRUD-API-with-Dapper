using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Dtos;
using WebAPI.Service.Services;

namespace WebAPI.Application.Controllers;

[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IService<OrderDto> _orderService;

    public OrderController(IService<OrderDto> orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var res = await _orderService.GetAsync();
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var res = await _orderService.GetByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPost]
    public async Task<IActionResult> InsertOrder([FromBody] OrderDto order)
    {
        var res = await _orderService.InsertAsync(order);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderById(int id, [FromBody] OrderDto order)
    {
        var res = await _orderService.UpdateByIdAsync(id, order);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderById(int id)
    {
        var res = await _orderService.DeleteByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
}