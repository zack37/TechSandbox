using System;
using System.Collections.Generic;
using Nancy.Security;

namespace ECommerceFX.Web
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; private set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid FormsAuthenticationGuid { get; set; }
        public IEnumerable<string> Claims { get; private set; }

        public UserIdentity(string userName, IEnumerable<string> claims)
        {
            UserName = userName;
            Claims = claims;
        }
    }
}