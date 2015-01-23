using ECommerceFX.Data.Messages.Queries;
using ECommerceFX.Web.Services;
using Ninject;
using NServiceBus;

namespace ECommerceFX.Web
{
    public class WebEndpointConfig : IConfigureThisEndpoint
    {
        private readonly IKernel _container;

        public WebEndpointConfig(IKernel container)
        {
            _container = container;
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("ECommerceFX.Web");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.AssembliesToScan(typeof (IService<>).Assembly, typeof (GetAllProductsQuery).Assembly);
            configuration.UseContainer<NinjectBuilder>(x => x.ExistingKernel(_container));
        }
    }
}