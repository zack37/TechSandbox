using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerceFX.Data;
using ECommerceFX.Web.Services;
using ECommerceFX.Web.Tools.Validation;
using ECommerceFX.Web.ViewModels;
using FluentValidation.Results;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace ECommerceFX.Web.Modules
{
    public class UserModule : NancyModule
    {
        private readonly IUserService _userService;
        private readonly IModelValidator _modelValidator;

        public UserModule(IUserService userService, IModelValidator modelValidator)
            : base("/users")
        {
            _userService = userService;
            _modelValidator = modelValidator;
            this.RequiresClaimsOnRoutesExcept(new[] {"admin"}, "/users/register", "/users/login");
            Get["/"] = Index;
            Get["/id/{id:guid}"] = ById;
            Post["/promote/id/{id:guid}"] = PromoteToAdmin;
            Get["/username/{username}"] = ByUsername;
            Get["/email/{email:email}"] = ByEmail;
            Get["/login"] = GetLogin;
            Post["/login", true] = PostLogin;
            Get["/register"] = GetRegister;
            Post["/register", true] = PostRegister;
            Post["/logout"] = Logout;
        }

        public dynamic Index(dynamic parameter)
        {
            return View["Views/User/Index.cshtml", _userService.All().ToList().AsEnumerable()];
        }

        public dynamic GetLogin(dynamic parameters)
        {
            ViewBag.Title = "Login";
            return View["Views/User/Login.cshtml", new LoginIndexViewModel()];
        }

        public async Task<dynamic> PostLogin(dynamic parameters, CancellationToken token)
        {
            var login = this.Bind<LoginViewModel>();
            var user = _userService.ByUsername(login.Username);
            var validator = await _modelValidator.ValidateAsync<LoginViewModelValidator>(login);
            if (user != null && validator.IsValid)
                return this.Login(user.Id);
            return View["Views/User/Login.cshtml",
                new LoginIndexViewModel(validator.Errors)];
        }

        public dynamic ById(dynamic parameters)
        {
            return View["Views/User/Index.cshtml", _userService.ById(parameters.Id)];
        }

        private dynamic PromoteToAdmin(dynamic parameters)
        {
            var user = _userService.ById((Guid) parameters.Id);
            user.Claims = new List<string> {"admin", "user"};
            _userService.Update(user);
            return Response.AsRedirect("~/");
        }

        public dynamic ByUsername(dynamic parameters)
        {
            return View["Views/User/Index.cshtml", _userService.ByUsername(parameters.Username)];
        }

        public dynamic ByEmail(dynamic parameters)
        {
            return View["Views/User/Index.cshtml", _userService.ByEmail(parameters.Email)];
        }

        public dynamic GetRegister(dynamic parameters)
        {
            ViewBag.Title = "Register";
            return View["Views/User/Register.cshtml", new RegisterViewModel()];
        }

        public async Task<dynamic> PostRegister(dynamic parameters, CancellationToken token)
        {
            var register = this.Bind<RegisterViewModel>();
            var validator = await _modelValidator.ValidateAsync<RegisterViewModelValidator>(register);

            if (!validator.IsValid)
                return View["Views/Users/Register.cshtml", new RegisterViewModel(validator.Errors)];

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = register.Username,
                Email = register.Email,
                Password = register.Password,
                Claims = new List<string> { "user" }
            };
            _userService.Create(newUser);

            return this.LoginAndRedirect(newUser.Id);
        }

        public dynamic Logout(dynamic parameters)
        {
            return this.LogoutAndRedirect("~/users/login");
        }
    }

    public abstract class ViewModel
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }

        protected ViewModel()
            : this(null) { }

        protected ViewModel(IEnumerable<ValidationFailure> errors)
        {
            Errors = errors ?? Enumerable.Empty<ValidationFailure>();
        }
    }

    public class LoginViewModel : ViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel() { }
        public LoginViewModel(IEnumerable<ValidationFailure> errors)
            : base(errors) { }
    }

    public class RegisterViewModel : ViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterViewModel() { }
        public RegisterViewModel(IEnumerable<ValidationFailure> errors)
            : base(errors) { }
    }
}