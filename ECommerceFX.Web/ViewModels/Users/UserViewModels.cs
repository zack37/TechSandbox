using System.Collections.Generic;
using System.Linq;
using ECommerceFX.Data;

namespace ECommerceFX.Web.ViewModels.Users
{
    public class UserViewModels
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public UserViewModels(IEnumerable<User> users)
        {
            Users = users.Select(u => new UserViewModel(u));
        }
    }
}