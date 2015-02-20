using ECommerceFX.Repository;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Diagnostics;
using Nancy.Responses.Negotiation;
using Ninject;
using Ninject.Extensions.Conventions;
using NServiceBus;

namespace ECommerceFX.Data.WebService
{
    public class WebServiceBootstrapper : NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            container.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IRepository<>))
                    .SelectAllClasses()
                    .InheritedFrom(typeof (IRepository<>))
                    .BindDefaultInterface()
                    .Configure(c => c.InTransientScope());

                x.FromThisAssembly()
                    .Select(typeof (IConfigureThisEndpoint).IsAssignableFrom)
                    .BindSingleInterface();
            });

            var config = container.Get<IConfigureThisEndpoint>();
            var busConfig = new BusConfiguration();
            config.Customize(busConfig);
            Bus.Create(busConfig).Start();

            base.ApplicationStartup(container, pipelines);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
                    // Force Json responses
                    c.ResponseProcessors.Clear();
                    c.ResponseProcessors.Add(typeof (JsonProcessor));
                });
            }
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration {Password = "password"}; }
        }
    }
}