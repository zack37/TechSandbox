using System;
using System.Collections.Generic;
using System.Linq;
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
            container.Bind(x => x.FromAssemblyContaining<IUserRepository>()
                .Select(type => type.Name.EndsWith("Repository"))
                .BindDefaultInterface()
                .Configure(c => c.InTransientScope()));
            container.Bind(x => x.FromThisAssembly()
                .Select(type => typeof (IConfigureThisEndpoint).IsAssignableFrom(type))
                .BindSingleInterface());

            container.GetAll<IConfigureThisEndpoint>().Select(x =>
            {
                var busConfig = new BusConfiguration();
                x.Customize(busConfig);
                return busConfig;
            }).Each(x => Bus.Create(x).Start());

            base.ApplicationStartup(container, pipelines);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
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

    public class WebServiceEndpointConfig : IConfigureThisEndpoint
    {
        private readonly IKernel _container;

        public WebServiceEndpointConfig(IKernel container)
        {
            _container = container;
        }
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("ECommerceFX.WebService");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseContainer<NinjectBuilder>(x => x.ExistingKernel(_container));
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
            return source;
        }
    }
}