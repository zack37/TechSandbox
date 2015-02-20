using ECommerceFX.Web.Tools;
using Microsoft.AspNet.SignalR.Hubs;

namespace ECommerceFX.Web.Consumers.Product
{
    public abstract class ProductConsumerBase
    {
        protected readonly IHub ProductHub;

        protected ProductConsumerBase(IHubProvider hubProvider)
        {
            ProductHub = hubProvider.GetHub<IProductHub>();
        }
    }
}