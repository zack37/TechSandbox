using System;
using ECommerceFX.Data;

namespace ECommerceFX.Web.Services
{
    public interface IUserService : IService<User>
    {
        User ByUsername(string username);
        User ByEmail(string email);
        bool UserExistsByUsername(string username);
        bool UserExistsByEmail(string email);
        bool UserExists(Guid id);
    }
}