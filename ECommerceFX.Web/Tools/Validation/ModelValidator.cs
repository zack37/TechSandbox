using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerceFX.Web.Tools.Validation
{
    public class ModelValidator : IModelValidator
    {
        private readonly IServiceProvider _container;

        public ModelValidator(IServiceProvider container)
        {
            _container = container;
        }

        public ValidationResult Validate(Type validatorType, object model)
        {
            if (!typeof (IValidator).IsAssignableFrom(validatorType))
                throw new ArgumentException("Parameter validatorType must be of type IValidator", "validatorType");

            var validator = (IValidator) _container.GetService(validatorType);
            var validationResult = validator.Validate(model);
            return validationResult;
        }

        public ValidationResult Validate<TValidator>(object model)
            where TValidator : class, IValidator
        {
            return Validate(typeof (TValidator), model);
        }

        public async Task<ValidationResult> ValidateAsync(Type validatorType, object model)
        { 
            if (!typeof (IValidator).IsAssignableFrom(validatorType))
                throw new ArgumentException("Parameter validatorType must be of type IValidator", "validatorType");

            var validator = (IValidator) _container.GetService(validatorType);
            var validationResult = await validator.ValidateAsync(model);
            return validationResult;
        }

        public async Task<ValidationResult> ValidateAsync<TValidator>(object model)
            where TValidator : class, IValidator
        {
            return await ValidateAsync(typeof (TValidator), model);
        }
    }
}