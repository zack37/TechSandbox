using NServiceBus;

namespace ECommerceFX.Data.Messages.Events
{
    public class ProductUpdated : IEvent
    {
        public Product Product;
    }
}