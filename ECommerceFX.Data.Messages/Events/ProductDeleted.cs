using System;
using NServiceBus;

namespace ECommerceFX.Data.Messages.Events
{
    public class ProductDeleted : IEvent
    {
        public Guid Id { get; set; }
    }
}