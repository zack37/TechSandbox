using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerceFX.Web.Tools.Validation
{
    public interface IModelValidator
    {
        ValidationResult Validate(Type validatorType, object model);

        ValidationResult Validate<TValidator>(object model)
            where TValidator : class, IValidator;

        Task<ValidationResult> ValidateAsync(Type validatorType, object model);

        Task<ValidationResult> ValidateAsync<TValidator>(object model)
            where TValidator : class, IValidator;
    }
}
