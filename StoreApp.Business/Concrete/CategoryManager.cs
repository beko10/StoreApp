using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StoreApp.Business.Abstract;
using StoreApp.Business.Exceptions;
using StoreApp.Business.Utilities.Constants;
using StoreApp.Business.Utilities.Helper;
using StoreApp.Business.Utilities.Result;
using StoreApp.DataAccess.UnitOfWork;
using StoreApp.Entities.Dto;
using StoreApp.Entities.Entity;

namespace StoreApp.Business.Concerete;

public class CategoryManager : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

    public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryValidator = updateCategoryValidator;
    }

    public async Task<DataResult<IEnumerable<GetCategoryDto>>> GetAllAsync(bool track = true)
    {
        var categoryList = await _unitOfWork.CategoryRepository.GetAll(track).ToListAsync();
        var categoryListMapping = _mapper.Map<IEnumerable<GetCategoryDto>>(categoryList);
        return DataResult<IEnumerable<GetCategoryDto>>.Success(categoryListMapping,ConstantMessages.CategoryMessages.CategoriesListed);
    }

    public  async Task<DataResult<GetCategoryDto>> GetByIdAsync(string id, bool track = true)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id, track);
        if(category == null)
        {
            throw new NotFoundException(ConstantMessages.CategoryMessages.CategoryNotFound);
        }
        var categoryMapping = _mapper.Map<GetCategoryDto>(category);
        return DataResult<GetCategoryDto>.Success(categoryMapping, ConstantMessages.CategoryMessages.CategoryFound);
    }

    public async Task<Result>  AddAsync(CreateCategoryDto createCategoryDto)
    {
        await ValidationHelper.ValidationWithFluent(_createCategoryValidator, createCategoryDto);
        var categoryMapping = _mapper.Map<Category>(createCategoryDto);
        await _unitOfWork.CategoryRepository.CreateAsync(categoryMapping);
        await _unitOfWork.SaveAsync();
        return Result.Success(ConstantMessages.CategoryMessages.CategoryCreated);
    }

    public async Task<Result>  DeleteAsync(string id)
    {
        var categoryExist = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if(categoryExist == null)
        {
            throw new NotFoundException(ConstantMessages.CategoryMessages.CategoryNotFound);
        }
        var categoryMapping = _mapper.Map<Category>(categoryExist);
        _unitOfWork.CategoryRepository.Delete(categoryMapping);
        await _unitOfWork.SaveAsync();
        return Result.Success(ConstantMessages.CategoryMessages.CategoryDeleted);
    }


    public async Task<Result>  UpdateAsync(UpdateCategoryDto updateCategoryDto)
    {
        if(updateCategoryDto.Id == null)
        {
            throw new ArgumentException(ConstantMessages.CategoryMessages.CategoryNotFound);
        }
        await ValidationHelper.ValidationWithFluent(_updateCategoryValidator, updateCategoryDto);
        var categoryExist = await _unitOfWork.CategoryRepository.GetByIdAsync(updateCategoryDto.Id);
        if(categoryExist == null)
        {
            throw new NotFoundException(ConstantMessages.CategoryMessages.CategoryNotFound);
        }
        _mapper.Map(updateCategoryDto, categoryExist);
        _unitOfWork.CategoryRepository.Update(categoryExist);
        await _unitOfWork.SaveAsync();
        return Result.Success(ConstantMessages.CategoryMessages.CategoryUpdated);
    }
}
