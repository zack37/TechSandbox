using System;
using ECommerceFX.Data;
using RestSharp;

namespace ECommerceFX.Web.Services
{
    public class UserService : RestService<User>, IUserService
    {
        public UserService(IRestClient client)
            : base(client, "/users") { }

        public User ByUsername(string username)
        {
            var request = new RestRequest(BaseUrl + "/username/{username}")
                .AddUrlSegment("username", username);
            return Request(request);
        }

        public User ByEmail(string email)
        {
            return null;
        }

        public bool UserExistsByUsername(string username)
        {
            return ByUsername(username) != null;
        }

        public bool UserExistsByEmail(string email)
        {
            return true;
        }

        public bool UserExists(Guid id)
        {
            return true;
        }
    }
}