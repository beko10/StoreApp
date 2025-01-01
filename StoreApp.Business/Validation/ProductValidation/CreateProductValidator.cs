using FluentValidation;
using StoreApp.Business.Utilities.Constants;
using StoreApp.Business.Validation.Helper;
using StoreApp.Entities.Dto;

namespace StoreApp.Business.Validation.ProductValidation;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı boş olamaz")
            .MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır")
            .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olmalıdır")
            .Matches(RegexPatterns.OnlyLettersAndNumbers).WithMessage("Ürün adı özel karakter içeremez");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Fiyat boş olamaz")
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır")
            .LessThan(1000000).WithMessage("Fiyat 1.000.000'dan küçük olmalıdır")
            .PrecisionScale(10, 2, false).WithMessage("Fiyat en fazla 2 ondalık basamak içerebilir");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage("Stok miktarı boş olamaz")
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı 0'dan küçük olamaz")
            .LessThan(100000).WithMessage("Stok miktarı 100.000'den küçük olmalıdır");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Ürün görseli boş olamaz")
            .Must(FileExtensionsHelper.IsValidImageExtension).WithMessage("Geçerli bir URL giriniz")
            .MaximumLength(500).WithMessage("URL 500 karakterden uzun olamaz");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Ürün açıklaması boş olamaz")
            .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Kategori seçilmek zorundadır");
    }


}