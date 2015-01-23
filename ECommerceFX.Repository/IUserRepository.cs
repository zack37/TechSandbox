using System;
using ECommerceFX.Data;

namespace ECommerceFX.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User ByEmail(string email);
        bool UserExists(Guid id);
        User ByUsername(string username);
        bool UserExistsByUsername(string username);
        bool UserExistsByEmail(string email);
    }
}