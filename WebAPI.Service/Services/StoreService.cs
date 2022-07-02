using AutoMapper;
using WebAPI.DAL.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Services;

public class StoreService : IService<StoreDto>
{
    private readonly IRepository<Store> _storeRepository;
    private readonly IMapper _mapper;

    public StoreService(IRepository<Store> storeRepository, IMapper mapper)
    {
        _storeRepository = storeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IList<StoreDto>>> GetAsync()
    {
        var stores = await _storeRepository.GetAllAsync();
        var storesRes = stores.Select(x => _mapper.Map<StoreDto>(x)).ToList();
        var res = ResponseDto<IList<StoreDto>>.Success(200, storesRes);
        return res;
    }

    public async Task<ResponseDto<StoreDto>> GetByIdAsync(int id)
    {
        var store = await _storeRepository.GetByIdAsync(id);
        if (store == null)
        {
            return ResponseDto<StoreDto>.Fail(404, "Store not found");
        }
        var storeRes = _mapper.Map<StoreDto>(store);
        var res = ResponseDto<StoreDto>.Success(200, storeRes);
        return res;
    }

    public async Task<ResponseDto<int>> InsertAsync(StoreDto entity)
    {
        var insertedId = await _storeRepository.InsertAsync(_mapper.Map<Store>(entity));
        var res = ResponseDto<int>.Success(201, insertedId);
        return res;
    }

    public async Task<ResponseDto<StoreDto>> UpdateByIdAsync(int id, StoreDto entity)
    {
        var affectedRowCount = await _storeRepository.UpdateByIdAsync(id, _mapper.Map<Store>(entity));
        if (affectedRowCount != 0)
        {
            return ResponseDto<StoreDto>.Success(204);
        }
        return ResponseDto<StoreDto>.Fail(404,"Store not found.");
    }

    public async Task<ResponseDto<StoreDto>> DeleteByIdAsync(int id)
    {
        var affectedRowCount = await _storeRepository.DeleteByIdAsync(id);
        if (affectedRowCount != 0)
        {
            return ResponseDto<StoreDto>.Success(204);
        }
        return ResponseDto<StoreDto>.Fail(404,"Store not found.");
    }
}