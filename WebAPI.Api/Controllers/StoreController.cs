using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Dtos;
using WebAPI.Service.Services;

namespace WebAPI.Application.Controllers;

[Route("api/[controller]")]
public class StoreController : Controller
{
    private readonly IService<StoreDto> _storeService;

    public StoreController(IService<StoreDto> storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await _storeService.GetAsync();
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var res = await _storeService.GetByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StoreDto store)
    {
        var res = await _storeService.InsertAsync(store);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] StoreDto store)
    {
        var res = await _storeService.UpdateByIdAsync(id, store);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _storeService.DeleteByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
}