using FluentValidation;

namespace StoreApp.Business.Utilities.Helper;


public static class ValidationHelper
{
    public static async Task ValidationWithFluent<T>(IValidator<T> validator,T entity)
    {
        var result = await validator.ValidateAsync(entity);
        if(!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}