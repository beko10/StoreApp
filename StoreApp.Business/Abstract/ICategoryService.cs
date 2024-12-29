using StoreApp.Business.Utilities.Result;
using StoreApp.Entities.Dto;

namespace StoreApp.Business.Abstract;

public interface ICategoryService
{
    Task<DataResult<IEnumerable<GetCategoryDto>>> GetAllAsync(bool track = true);
    Task<DataResult<GetCategoryDto>> GetByIdAsync(string id,bool track = true);
    Task<Result> AddAsync(CreateCategoryDto createCategoryDto);
    Task<Result> UpdateAsync(UpdateCategoryDto updateCategoryDto);
    Task<Result> DeleteAsync(string id);
}