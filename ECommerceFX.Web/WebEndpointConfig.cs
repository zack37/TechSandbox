﻿using Ninject;
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
//            configuration.AssembliesToScan(AllAssemblies.Matching("ECommerceFX.Data.Messages.").And("ECommerceFX.Web."));
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseContainer<NinjectBuilder>(x => x.ExistingKernel(_container));
            configuration.Transactions().Disable().DisableDistributedTransactions();
        }
    }
}