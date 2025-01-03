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

public class ProductManager : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProductDto> _createProductValidator;
    private readonly IValidator<UpdateProductDto> _updateProductValidator;
    private readonly IFileService _fileService;
    public ProductManager(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateProductDto> createProductValidator, IValidator<UpdateProductDto> updateProductValidator, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createProductValidator = createProductValidator;
        _updateProductValidator = updateProductValidator;
        _fileService = fileService;
    }
    public async Task<DataResult<IEnumerable<GetProductDto>>> GetAllAsync(bool track = true)
    {
        var productList = await _unitOfWork.ProductRepository.GetAll(track).ToListAsync();
        if (productList.Count() < -1)
        {
            throw new NotFoundException(ConstantMessages.ProductMessages.ProductListNotFound);
        }

        var productListMapping = _mapper.Map<IEnumerable<GetProductDto>>(productList);
        return DataResult<IEnumerable<GetProductDto>>.Success(productListMapping, ConstantMessages.ProductMessages.ProductsListed);

    }

    public async Task<DataResult<GetProductDto>> GetByIdAsync(string id, bool track = true)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, track);
        if (product == null)
        {
            throw new NotFoundException(ConstantMessages.ProductMessages.ProductNotFound);
        }
        var productMapping = _mapper.Map<GetProductDto>(product);
        return DataResult<GetProductDto>.Success(productMapping, ConstantMessages.ProductMessages.ProductFound);
    }
    public async Task<Result> AddAsync(CreateProductDto createProductDto)
    {

        await ValidationHelper.ValidationWithFluent(_createProductValidator, createProductDto);
        var productMapping = _mapper.Map<Product>(createProductDto);
        await _unitOfWork.ProductRepository.CreateAsync(productMapping);
        await _unitOfWork.SaveAsync();
        return Result.Success(ConstantMessages.ProductMessages.ProductCreated);
    }

    public async Task<Result> DeleteAsync(string id)
    {
        var productExist = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (productExist == null)
        {
            throw new NotFoundException(ConstantMessages.ProductMessages.ProductNotFound);
        }
        var productMapping = _mapper.Map<Product>(productExist);
        _unitOfWork.ProductRepository.Delete(productMapping);
        await _unitOfWork.SaveAsync();
        return Result.Success(ConstantMessages.ProductMessages.ProductDeleted);
    }
    public async Task<Result> UpdateAsync(UpdateProductDto updateProductDto)
    {
        if (updateProductDto.Id == null)
        {
            throw new ArgumentException("Id alanı boş olamaz.");
        }
        await ValidationHelper.ValidationWithFluent(_updateProductValidator, updateProductDto);
        var productExist = await _unitOfWork.ProductRepository.GetByIdAsync(updateProductDto.Id);
        if (productExist == null)
        {
            throw new NotFoundException(ConstantMessages.ProductMessages.ProductNotFound);
        }
        _mapper.Map(updateProductDto, productExist);
        _unitOfWork.ProductRepository.Update(productExist);
        await _unitOfWork.SaveAsync();
        return Result.Success(ConstantMessages.ProductMessages.ProductUpdated);
    }

    public async Task<DataResult<int>> GetProductCountAsync(bool track = true)
    {
        var productCount = await _unitOfWork.ProductRepository.GetTotalProductCountAsync(false);
        if (productCount > -1)
        {
            return DataResult<int>.Success(productCount, ConstantMessages.ProductMessages.ProductsListed);
        }
        return DataResult<int>.Error(-1, ConstantMessages.ProductMessages.ProductListNotFound);
    }

    public async Task<DataResult<IEnumerable<GetProductDetailDto>>> GetProductsDetailAsync(bool track = true)
    {
        var productsDetail = await _unitOfWork.ProductRepository.GetAllProductsDetail(track).ToListAsync();
        if (!productsDetail.Any())
        {
            throw new NotFoundException(ConstantMessages.ProductMessages.ProductListNotFound);
        }
        var productsDetailMapping = _mapper.Map<IEnumerable<GetProductDetailDto>>(productsDetail);
        return DataResult<IEnumerable<GetProductDetailDto>>.Success(productsDetailMapping, ConstantMessages.ProductMessages.ProductsListed);
    }

    public async Task<DataResult<GetProductDetailDto>> GetProductDetailByIdAsync(string id, bool track = true)
    {
        var productDetail = await _unitOfWork.ProductRepository.GetProductDetailByIdAsync(id, track);
        if (productDetail == null)
        {
            throw new NotFoundException(ConstantMessages.ProductMessages.ProductNotFound);
        }
        var productDetailMapping = _mapper.Map<GetProductDetailDto>(productDetail);

        // if (!string.IsNullOrEmpty(productDetailMapping.ImageUrl) && !productDetailMapping.ImageUrl.StartsWith("/"))
        // {
        //     productDetailMapping.ImageUrl = "/" + productDetailMapping.ImageUrl;
        // }
        return DataResult<GetProductDetailDto>.Success(productDetailMapping, ConstantMessages.ProductMessages.ProductFound);
    }
}
