using System.Collections.Generic;
using ECommerceFX.Web.Modules;
using FluentValidation.Results;

namespace ECommerceFX.Web.ViewModels
{
    public class LoginIndexViewModel : ViewModel
    {
        public LoginIndexViewModel() { }

        public LoginIndexViewModel(IEnumerable<ValidationFailure> errors)
            : base(errors) { }
    }
}