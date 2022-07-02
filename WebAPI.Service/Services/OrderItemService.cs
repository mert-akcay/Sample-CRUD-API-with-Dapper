using AutoMapper;
using WebAPI.DAL.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Services;

public class OrderItemService : IService<OrderItemDto>
{
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IRepository<OrderItem> orderItemRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IList<OrderItemDto>>> GetAsync()
    {
        var orderItems = await _orderItemRepository.GetAllAsync();
        var orderItemsRes = orderItems.Select(x => _mapper.Map<OrderItemDto>(x)).ToList();
        return ResponseDto<IList<OrderItemDto>>.Success(200, orderItemsRes);
    }

    public async Task<ResponseDto<OrderItemDto>> GetByIdAsync(int id)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
        {
            return ResponseDto<OrderItemDto>.Fail(404,"Order Item not found.");
        }
        var orderItemRes = _mapper.Map<OrderItemDto>(orderItem);
        return ResponseDto<OrderItemDto>.Success(200, orderItemRes);
    }

    public async Task<ResponseDto<int>> InsertAsync(OrderItemDto entity)
    {
        var insertedId = await _orderItemRepository.InsertAsync(_mapper.Map<OrderItem>(entity));
        return ResponseDto<int>.Success(201, insertedId);
    }

    public async Task<ResponseDto<OrderItemDto>> UpdateByIdAsync(int id, OrderItemDto entity)
    {
        var affectedRowCount = await _orderItemRepository.UpdateByIdAsync(id, _mapper.Map<OrderItem>(entity));
        if (affectedRowCount != 0)
        {
            return ResponseDto<OrderItemDto>.Success(204);
        }
        return ResponseDto<OrderItemDto>.Fail(404,"Order Item not found.");
    }

    public async Task<ResponseDto<OrderItemDto>> DeleteByIdAsync(int id)
    {
        var affectedRowCount = await _orderItemRepository.DeleteByIdAsync(id);
        if (affectedRowCount != 0)
        {
            return ResponseDto<OrderItemDto>.Success(204);
        }
        return ResponseDto<OrderItemDto>.Fail(404,"Order Item not found.");
    }
}