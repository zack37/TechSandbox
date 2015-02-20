using NServiceBus;

namespace ECommerceFX.Data.Messages.Commands
{
    public class CreateUser : ICommand
    {
        public User User { get; set; }
    }
}