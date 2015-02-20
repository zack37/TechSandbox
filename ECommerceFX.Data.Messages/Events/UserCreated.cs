using NServiceBus;

namespace ECommerceFX.Data.Messages.Events
{
    public class UserCreated : IEvent
    {
        public User User { get; set; }
    }
}