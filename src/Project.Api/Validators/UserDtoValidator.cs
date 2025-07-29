using FluentValidation;
using Project.Application.Dtos.Users;

namespace Project.Api.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(entity => entity.FirstName)
            .NotNull()
            .NotEmpty();


        RuleFor(entity => entity.LastName)
            .NotEmpty()
            .NotNull();
    }
}