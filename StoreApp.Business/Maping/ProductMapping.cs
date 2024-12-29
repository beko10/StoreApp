using AutoMapper;
using StoreApp.Entities.Dto;
using StoreApp.Entities.Entity;

namespace StoreApp.Business.Mapping;
public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, GetProductDto>()
            .ReverseMap();
        CreateMap<Product, CreateProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
            .ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
    }
}
