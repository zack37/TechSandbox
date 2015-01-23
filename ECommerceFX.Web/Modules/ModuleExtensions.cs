using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Extensions;
using Nancy.Security;

namespace ECommerceFX.Web.Modules
{
    public static class ModuleExtensions
    {
        public static void RequiresAuthenticationExcept(this NancyModule module, params string[] except)
        {
            var reason = string.Format("Requires authentication except on routes {0}", string.Join(", ", except));
            module.AddBeforeHookOrExecute(ctx =>
            {
                Response response = null;
                if (!except.Contains(ctx.ResolvedRoute.Description.Path) && !ctx.CurrentUser.IsAuthenticated())
                {
                    response = new Response {StatusCode = HttpStatusCode.Unauthorized};
                }
                return response;
            }, reason);
        }

        public static void RequiresClaimsOnRoutesExcept(this NancyModule module, IEnumerable<string> claims, params string[] except)
        {
            var reason = string.Format("Requires claims {0} except on routes {1}", string.Join(", ", claims),
                string.Join(", ", except));
            module.AddBeforeHookOrExecute(ctx =>
            {
                Response response = null;
                if (!except.Contains(ctx.ResolvedRoute.Description.Path) && !ctx.CurrentUser.IsAuthenticated() &&
                    !ctx.CurrentUser.HasClaims(claims))
                {
                    response = new Response {StatusCode = HttpStatusCode.Unauthorized};
                }
                return response;
            }, reason);
        }
    }
}