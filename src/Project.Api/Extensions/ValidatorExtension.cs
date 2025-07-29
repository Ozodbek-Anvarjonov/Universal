using FluentValidation;
using FluentValidation.Results;

namespace Project.Api.Extensions;

public static class ValidatorExtension
{
    public static async Task<ValidationResult> EnsureValidationAsync<TObject>(
        this IValidator<TObject> validator,
        TObject instance,
        CancellationToken cancellationToken = default)
    {
        var result = await validator.ValidateAsync(instance, cancellationToken);

        if (!result.IsValid)
            throw new Application.Common.Exceptions.ValidationException(GetErrors(result));

        return result;
    }

    private static IDictionary<string, string[]> GetErrors(ValidationResult result)
    {
        return result.Errors
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage).ToArray()
            );
    }
}