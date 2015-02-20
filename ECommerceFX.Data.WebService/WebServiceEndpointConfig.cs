using ECommerceFX.Data.Messages.Queries;
using Ninject;
using NServiceBus;

namespace ECommerceFX.Data.WebService
{
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
//            configuration.AssembliesToScan(AllAssemblies.Matching("ECommerceFX.Data.Messages.").And("ECommerceFX.WebService."));
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseContainer<NinjectBuilder>(x => x.ExistingKernel(_container));
            configuration.Transactions().Disable().DisableDistributedTransactions();
        }
    }
}