using StoreApp.Business.Utilities.Result;
using StoreApp.Entities.Dto;
namespace StoreApp.Business.Abstract;
public interface IProductService
{
    Task<DataResult<IEnumerable<GetProductDto>>> GetAllAsync(bool track = true);
    Task<DataResult<GetProductDto>> GetByIdAsync(string id,bool track = true);
    Task<Result> AddAsync(CreateProductDto createProductDto);
    Task<Result> UpdateAsync(UpdateProductDto updateProductDto);
    Task<Result> DeleteAsync(string id);
    Task<DataResult<int>> GetProductCountAsync(bool track = true);
    Task<DataResult<IEnumerable<GetProductDetailDto>>> GetProductsDetailAsync(bool track = true);
    Task<DataResult<GetProductDetailDto>> GetProductDetailByIdAsync(string id, bool track = true);
}