using FluentValidation;
using StoreApp.Business.Utilities.Constants;
using StoreApp.Entities.Dto;


namespace StoreApp.Business.Validation.CategoryValidation;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
   public CreateCategoryValidator()
   {
       RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Kategori adı boş olamaz")
           .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır")
           .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olmalıdır")
           .Matches(RegexPatterns.OnlyLetters).WithMessage("Kategori adı sadece harf içerebilir");

        RuleFor(x => x.Description)
           .NotEmpty().WithMessage("Kategori açıklaması boş olamaz")
           .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır")
           .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır");
   }
}
