using System;
using ECommerceFX.Web.Services;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using NLog;

namespace ECommerceFX.Web
{
    public class DatabaseUser : IUserMapper
    {
        private readonly IUserDataService _userService;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public DatabaseUser(IUserDataService userService)
        {
            _userService = userService;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            Log.Debug("Getting user with identifier {0}", identifier);
            var user = _userService.ById(identifier);
            return user != null ? new UserIdentity(user.Username, user.Claims) : null;
        }
    }
}