using WebAPI.Service.Dtos;

namespace WebAPI.Service.Services;

public interface IService<TEntityDto>
{
    public Task<ResponseDto<IList<TEntityDto>>> GetAsync();
    public Task<ResponseDto<TEntityDto>> GetByIdAsync(int id);
    public Task<ResponseDto<int>> InsertAsync(TEntityDto entity);
    public Task<ResponseDto<TEntityDto>> UpdateByIdAsync(int id, TEntityDto entity);
    public Task<ResponseDto<TEntityDto>> DeleteByIdAsync(int id);
}