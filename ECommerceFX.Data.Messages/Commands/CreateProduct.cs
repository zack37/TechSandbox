﻿using NServiceBus;

namespace ECommerceFX.Data.Messages.Commands
{
    public class CreateProduct : ICommand
    {
        public Product Product { get; set; }
    }
}
