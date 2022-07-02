using AutoMapper;
using WebAPI.DAL.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Services;

public class CustomerService : IService<CustomerDto>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(IRepository<Domain.Entities.Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IList<CustomerDto>>> GetAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        var customersRes = customers.Select(c => _mapper.Map<CustomerDto>(c)).ToList();
        var res = ResponseDto<IList<CustomerDto>>.Success(200,customersRes);
        return res;
    }

    public async Task<ResponseDto<CustomerDto>> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            return ResponseDto<CustomerDto>.Fail(404,"Customer not found.");
        }
        var customerRes = _mapper.Map<CustomerDto>(customer);
        var res = ResponseDto<CustomerDto>.Success(200,customerRes);
        return res;
    }

    public async Task<ResponseDto<int>> InsertAsync(CustomerDto customer)
    {
        var insertedId = await _customerRepository.InsertAsync(_mapper.Map<Customer>(customer));
        return ResponseDto<int>.Success(201,insertedId);
    }

    public async Task<ResponseDto<CustomerDto>> UpdateByIdAsync(int id, CustomerDto customer)
    {
        var affectedRowCount =
            await _customerRepository.UpdateByIdAsync(id, _mapper.Map<Customer>(customer));
        if (affectedRowCount != 0)
        {
            return ResponseDto<CustomerDto>.Success(204);
        }
        return ResponseDto<CustomerDto>.Fail(404,"Customer not found.");
    }


    public async Task<ResponseDto<CustomerDto>> DeleteByIdAsync(int id)
    {
        var affectedRowCount = await _customerRepository.DeleteByIdAsync(id);
        if (affectedRowCount != 0)
        {
            return ResponseDto<CustomerDto>.Success(204);
        }
        return ResponseDto<CustomerDto>.Fail(404,"Customer not found.");
    }
}