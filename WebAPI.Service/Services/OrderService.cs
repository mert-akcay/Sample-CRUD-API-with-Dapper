using AutoMapper;
using WebAPI.DAL.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Services;

public class OrderService : IService<OrderDto>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IRepository<Order> orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IList<OrderDto>>> GetAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        var ordersRes = orders.Select(x => _mapper.Map<OrderDto>(x)).ToList();
        var res = ResponseDto<IList<OrderDto>>.Success(200, ordersRes);
        return res;
    }

    public async Task<ResponseDto<OrderDto>> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return ResponseDto<OrderDto>.Fail(404,"Order not found.");
        }
        var orderRes = _mapper.Map<OrderDto>(order);
        var res = ResponseDto<OrderDto>.Success(200, orderRes);
        return res;
    }

    public async Task<ResponseDto<int>> InsertAsync(OrderDto entity)
    {
        var insertedId = await _orderRepository.InsertAsync(_mapper.Map<Order>(entity));
        var res = ResponseDto<int>.Success(201, insertedId);
        return res;
    }

    public async Task<ResponseDto<OrderDto>> UpdateByIdAsync(int id, OrderDto entity)
    {
        var affectedRowCount = await _orderRepository.UpdateByIdAsync(id, _mapper.Map<Order>(entity));
        if (affectedRowCount != 0)
        {
            return ResponseDto<OrderDto>.Success(204);
        }
        return ResponseDto<OrderDto>.Fail(404,"Order not found.");
    }

    public async Task<ResponseDto<OrderDto>> DeleteByIdAsync(int id)
    {
        var affectedRowCount = await _orderRepository.DeleteByIdAsync(id);
        if (affectedRowCount != 0)
        {
            return ResponseDto<OrderDto>.Success(204);
        }
        return ResponseDto<OrderDto>.Fail(404,"Order not found.");
    }
}