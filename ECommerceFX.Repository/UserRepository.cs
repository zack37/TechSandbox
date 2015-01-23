using System;
using ECommerceFX.Data;

namespace ECommerceFX.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public User ByEmail(string email)
        {
            return ByKey(u => u.Email == email);
        }

        public bool UserExists(Guid id)
        {
            return ById(id) != null;
        }

        public User ByUsername(string username)
        {
            return ByKey(u => u.Username == username);
        }

        public bool UserExistsByUsername(string username)
        {
            return ByUsername(username) != null;
        }

        public bool UserExistsByEmail(string email)
        {
            return ByKey(u => u.Email == email) != null;
        }
    }
}