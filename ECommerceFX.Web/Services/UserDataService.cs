using System;
using System.Collections.Generic;
using ECommerceFX.Data;
using ECommerceFX.Data.Messages.Commands;
using ECommerceFX.Data.Messages.Queries;
using NServiceBus;

namespace ECommerceFX.Web.Services
{
    public class UserDataService : DataService<User>, IUserDataService
    {
        public UserDataService(IBus bus)
            : base(bus) { }

        public IEnumerable<User> All()
        {
            return All<GetAllUsersQuery, GetAllUsersResponse>(response => response.Users);
        }

        public User ById(Guid id)
        {
            return ById<GetUserByIdQuery, GetUserByIdResponse>(id, response => response.User);
        }

        public User Create(User user)
        {
            return Create<CreateUser>(user, command => command.User = user);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public User ByUsername(string username)
        {
            return ByKey<GetUserByUsernameQuery, GetUserByUsernameResponse>(x => x.Username = username, response => response.User);
        }

        public User ByEmail(string email)
        {

            return ByKey<GetUserByEmailQuery, GetUserByEmailResponse>(request => request.Email = email, response => response.User);
        }

        public bool UserExistsByUsername(string username)
        {
            return ByUsername(username) != null;
        }

        public bool UserExistsByEmail(string email)
        {
            return ByEmail(email) != null;
        }

        public bool UserExists(Guid id)
        {
            return ById<GetUserByIdQuery, GetUserByIdResponse>(id, response => response.User) != null;
        }
    }
}