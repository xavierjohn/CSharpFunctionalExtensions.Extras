namespace CSharpFunctionalExtensions.Errors;

using System.Linq;
using FluentValidation;

public static class FluentValidationExtension
{
    public static Result<T, ErrorList> ToResult<T>(this FluentValidation.Results.ValidationResult validationResult, T value)
    {
        if (validationResult.IsValid)
            return Result.Success<T, ErrorList>(value);

        var errors = validationResult.Errors
            .Select(x => Error.Validation(x.PropertyName, x.ErrorMessage));

        return Result.Failure<T, ErrorList>(new ErrorList(errors));
    }

    public static Result<T, ErrorList> ValidateToResult<T>(this IValidator<T> validator, T value) =>
        validator.Validate(value).ToResult(value);
}
