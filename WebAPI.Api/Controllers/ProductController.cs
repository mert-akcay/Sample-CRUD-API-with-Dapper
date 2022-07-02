using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Dtos;
using WebAPI.Service.Services;

namespace WebAPI.Application.Controllers;

[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IService<ProductDto> _productService;

    public ProductController(IService<ProductDto> productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var res = await _productService.GetAsync();
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var res = await _productService.GetByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
    {
        var res = await _productService.InsertAsync(product);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto product)
    {
        var res = await _productService.UpdateByIdAsync(id, product);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var res = await _productService.DeleteByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    }
}