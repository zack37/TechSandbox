using System;
using System.Collections.Generic;
using ECommerceFX.Data;

namespace ECommerceFX.Web.Services
{
    public interface IUserDataService : IDataService<User>
    {
        IEnumerable<User> All();
        User ById(Guid id);
        User Create(User user);
        void Delete(Guid id);
        void Update(User user);
        User ByUsername(string username);
        User ByEmail(string email);
        bool UserExistsByUsername(string username);
        bool UserExistsByEmail(string email);
        bool UserExists(Guid id);
    }
}