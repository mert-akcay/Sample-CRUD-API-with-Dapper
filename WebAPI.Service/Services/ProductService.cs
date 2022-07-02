using AutoMapper;
using WebAPI.DAL.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Services;

public class ProductService : IService<ProductDto>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Domain.Entities.Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IList<ProductDto>>> GetAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productsDto = _mapper.Map<IList<ProductDto>>(products);
        var res = ResponseDto<IList<ProductDto>>.Success(200, productsDto);
        return res;
    }

    public async Task<ResponseDto<ProductDto>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return ResponseDto<ProductDto>.Fail(404, "Product not found");
        }
        var productDto = _mapper.Map<ProductDto>(product);
        var res = ResponseDto<ProductDto>.Success(200, productDto);
        return res;
    }

    public async Task<ResponseDto<int>> InsertAsync(ProductDto product)
    {
        var insertedId = await _productRepository.InsertAsync(_mapper.Map<Product>(product));
        var res = ResponseDto<int>.Success(201, insertedId);
        return res;
    }

    public async Task<ResponseDto<ProductDto>> UpdateByIdAsync(int id, ProductDto customer)
    {
        var affectedRowCount = await _productRepository.UpdateByIdAsync(id, _mapper.Map<Product>(customer));
        if (affectedRowCount != 0)
        {
            return ResponseDto<ProductDto>.Success(204);
        }
        return ResponseDto<ProductDto>.Fail(404,"Product not found.");
    }

    public async Task<ResponseDto<ProductDto>> DeleteByIdAsync(int id)
    {
        var affectedRowCount = await _productRepository.DeleteByIdAsync(id);
        if (affectedRowCount != 0)
        {
            return ResponseDto<ProductDto>.Success(204);
        }
        return ResponseDto<ProductDto>.Fail(404,"Product not found.");
    }
}