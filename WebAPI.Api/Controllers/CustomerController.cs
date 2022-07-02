using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Dtos;
using WebAPI.Service.Services;

namespace WebAPI.Application.Controllers;

[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly IService<CustomerDto> _customerService;

    public CustomerController(IService<CustomerDto> customerService)
    {
      _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
      var res = await _customerService.GetAsync();
      return StatusCode(res.StatusCode, res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
      var res = await _customerService.GetByIdAsync(id);
      return StatusCode(res.StatusCode, res);
    }
    
    [HttpPost]
    public async Task<IActionResult> InsertCustomer([FromBody] CustomerDto customer)
    {
      var res = await _customerService.InsertAsync(customer);
      return StatusCode(res.StatusCode, res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomerById(int id, [FromBody] CustomerDto customer)
    {
      var res = await _customerService.UpdateByIdAsync(id, customer);
      return StatusCode(res.StatusCode, res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(int id)
    {
      var res = await _customerService.DeleteByIdAsync(id);
      return StatusCode(res.StatusCode, res);
    }
}