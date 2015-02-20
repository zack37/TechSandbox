using ECommerceFX.Data.Messages.Events;
using ECommerceFX.Web.Tools;
using NServiceBus;

namespace ECommerceFX.Web.Consumers.Product
{
    public class ProductDeletedConsumer : ProductConsumerBase, IHandleMessages<ProductDeleted>
    {
        public ProductDeletedConsumer(IHubProvider hubProvider)
            : base(hubProvider)
        {
        }

        public void Handle(ProductDeleted message)
        {
            ProductHub.Clients.Others.broadcastProductDeleted(message.Id);
        }
    }
}