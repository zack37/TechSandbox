using System;
using System.Collections.Generic;
using NServiceBus;

namespace ECommerceFX.Data.Messages.Queries
{
    public class GetAllProductsQuery : IMessage { }

    public class GetAllProductsResponse : IMessage
    {
        public IEnumerable<Product> Products { get; set; }
    }

    public class GetProductByIdQuery : IMessage
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdResponse : IMessage
    {
        public Product Product { get; set; }
    }
}
