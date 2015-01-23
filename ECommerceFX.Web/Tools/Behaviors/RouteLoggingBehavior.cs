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
                Log.Info("Invoking handler at route {0}", ctx.Request.Path);
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