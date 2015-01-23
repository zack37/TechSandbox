using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ECommerceFX.Data;
using ECommerceFX.Web.ViewModels.Products;

namespace ECommerceFX.Web.ViewModels.Users
{
    public class UserViewModel : DatabaseObjectViewModel<User>
    {
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

        public ProductViewModels Products { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.Password;
            Products = new ProductViewModels(user.Products);
        }
    }
}