using System;
using NServiceBus;

namespace ECommerceFX.Data.Messages.Events
{
    public class ProductCreated : IEvent
    {
        public Product Product { get; set; }
    }
    
    public class ProductDeleted : IEvent
    {
        public Guid Id { get; set; }
    }
}
