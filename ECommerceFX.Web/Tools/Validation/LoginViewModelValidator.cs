using ECommerceFX.Web.Modules;
using ECommerceFX.Web.Services;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerceFX.Web.Tools.Validation
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator(IUserDataService userService)
        {
            var isValid = true;

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username must have a value")
                .OnAnyFailure(x => isValid = false);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password must have a value")
                .OnAnyFailure(x => isValid = false);

            When(x => isValid, () => Custom(x =>
            {
                var user = userService.ByUsername(x.Username);
                if (user != null && user.Password == x.Password)
                    return null;
                return new ValidationFailure("Username,Password", "Username or Password is not recognized.");

            }));
        }
    }
}