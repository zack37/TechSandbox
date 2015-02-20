using System;
using NServiceBus;

namespace ECommerceFX.Data.Messages.Commands
{
    public class DeleteProduct : ICommand
    {
        public Guid Id { get; set; }
    }
}