using ECommerceFX.Data.Messages;
using ECommerceFX.Repository;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace ECommerceFX.Data.WebService
{
    public class UsersRestModule : NancyModule
    {
        private readonly IUserRepository _userRepository;

        public UsersRestModule(IUserRepository userRepository)
            :base("/users")
        {
            _userRepository = userRepository;
            Get["/"] = All;
            Get["/all"] = All;
            Get["/username/{username}"] = ByUsername;
            Post["create"] = CreateUser;
        }

        public dynamic All(dynamic parameters)
        {
            return _userRepository.All();
        }

        public dynamic ByUsername(dynamic parameters)
        {
            var user = _userRepository.ByUsername(parameters.Username);
            var response = new RestResponse<User>();
            if (user == null)
            {
                response.IsError = true;
                response.Message = "Could not find user with username " + parameters.Username;
                response.StatusCode = HttpStatusCode.NoContent;
            }
            response.Data = user;
            return response;
        }

        public dynamic CreateUser(dynamic parameters)
        {
            var user = this.Bind<User>();
            user = _userRepository.Create(user);
            var response = new RestResponse<User>(user);
            return response;
        }
    }
}