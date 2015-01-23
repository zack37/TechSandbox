using ECommerceFX.Web.Modules;
using ECommerceFX.Web.Services;
using FluentValidation;

namespace ECommerceFX.Web.Tools.Validation
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator(IUserService userService)
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username must have a value")
                .Length(8, 32)
                .WithMessage("Username must be between 8 and 32 characters")
                .Matches(ValidationConstants.Username)
                .WithMessage("Username can only contain letters, numbers, underscores, and periods")
                .Must(x => !userService.UserExistsByUsername(x))
                .WithMessage("Username already exists");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email must have a value")
                .EmailAddress()
                .Must(x => !userService.UserExistsByEmail(x))
                .WithMessage("Email already exists");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password must have a value")
                .Length(8, 20)
                .WithMessage("Password must be between 8 and 20 characters");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .WithMessage("Confirm Password must have a value")
                .Equal(x => x.Password)
                .WithMessage("Confirm Password must be the same as Password");
        }
    }
}