using System;
using NServiceBus;

namespace ECommerceFX.Data.Messages.Commands
{
    public class UpdateProduct : ICommand
    {
        public Product Product { get; set; }
    }
}