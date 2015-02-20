using System;
using System.Linq;
using ECommerceFX.Web.Consumers.Product;
using ECommerceFX.Web.Services;
using ECommerceFX.Web.Tools;
using ECommerceFX.Web.Tools.Validation;
using Microsoft.AspNet.SignalR.Hubs;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Ninject;
using Ninject.Extensions.Conventions;
using NLog;
using NServiceBus;
using RestSharp;

namespace ECommerceFX.Web
{
    public class WebBootstrapper : NinjectNancyBootstrapper
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            Log.Trace("Setting up application");

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureApplicationContainer(IKernel container)
        {
            Log.Trace("Configuring container");
            container.Bind<IUserMapper>().To<DatabaseUser>();
            container.Bind(x =>
            {
                x.FromAssemblyContaining(typeof (IDataService<>))
                    .SelectAllClasses()
                    .InheritedFrom(typeof (IDataService<>))
                    .BindDefaultInterface()
                    .Configure(c => c.InSingletonScope());

                x.FromThisAssembly()
                    .Select(typeof (IConfigureThisEndpoint).IsAssignableFrom)
                    .BindSingleInterface();

                x.FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IHub>()
                    .BindDefaultInterface()
                    .Configure(c => c.InSingletonScope());
            });
            container.Bind<IServiceProvider>().ToConstant(container);
            container.Bind<IModelValidator>().To<ModelValidator>().InSingletonScope();
            container.Bind<IRestClient>().To<RestClient>().InSingletonScope();
            container.Bind<IHubProvider>().To<HubProvider>().InSingletonScope();

            var configure = container.Get<IConfigureThisEndpoint>();
            var busConfig = new BusConfiguration();
            configure.Customize(busConfig);
            Bus.Create(busConfig).Start();

            base.ConfigureApplicationContainer(container);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            Log.Trace("Configuring conventions");
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Content/scripts", null, "js", "jsx"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Content/styles", null, "css"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Scripts", null, "js"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/Content/images"));
            base.ConfigureConventions(nancyConventions);
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration {Password = "password"}; }
        }
    }
}