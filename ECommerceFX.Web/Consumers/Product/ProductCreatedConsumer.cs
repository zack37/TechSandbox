using ECommerceFX.Data.Messages.Events;
using ECommerceFX.Web.Tools;
using ECommerceFX.Web.ViewModels.Products;
using NServiceBus;

namespace ECommerceFX.Web.Consumers.Product
{
    public class ProductCreatedConsumer : ProductConsumerBase, IHandleMessages<ProductCreated>
    {
        public ProductCreatedConsumer(IHubProvider hubProvider)
            :base(hubProvider)
        {
        }

        public void Handle(ProductCreated message)
        {
            ProductHub.Clients.Others.broadcastProductCreated(new ProductViewModel(message.Product));
        }
    }
}