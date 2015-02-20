using System.Text;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Ninject;
using NLog;

namespace ECommerceFX.Web.Tools.Behaviors
{
    public class RouteLoggingBehavior : IApplicationStartup
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public void Initialize(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                var sb = new StringBuilder();
                sb.AppendFormat("Invoking handler at route {0}", ctx.Request.Path);
                if (ctx.CurrentUser != null)
                {
                    sb.AppendFormat(" for user {0}", ctx.CurrentUser.UserName);
                }
                Log.Info(sb.ToString());
                return ctx.Response;
            });
        }
    }

    public class FormsAuthenticationBehavior : IApplicationStartup
    {
        private readonly IKernel _container;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public FormsAuthenticationBehavior(IKernel container)
        {
            _container = container;
        }

        public void Initialize(IPipelines pipelines)
        {
            Log.Info("Enabling Forms Authentication");
            FormsAuthentication.Enable(pipelines, new FormsAuthenticationConfiguration
            {
                RedirectUrl = "~/users/login",
                UserMapper = _container.Get<IUserMapper>()
            });
        }
    }
}