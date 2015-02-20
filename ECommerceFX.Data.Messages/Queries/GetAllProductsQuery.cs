using System;
using System.Collections.Generic;
using NServiceBus;

namespace ECommerceFX.Data.Messages.Queries
{
    public interface IHaveIdMessage : IMessage
    {
        Guid Id { get; set; }
    }

    public class GetAllProductsQuery : IMessage { }

    public class GetAllProductsResponse : IMessage
    {
        public IEnumerable<Product> Products { get; set; }
    }

    public class GetProductByIdQuery : IHaveIdMessage
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdResponse : IMessage
    {
        public Product Product { get; set; }
    }

    public class GetAllUsersQuery : IMessage { }

    public class GetAllUsersResponse : IMessage
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class GetUserByUsernameQuery : IMessage
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameResponse : IMessage
    {
        public User User { get; set; }
    }

    public class GetUserByEmailQuery : IMessage
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailResponse : IMessage
    {
        public User User { get; set; }
    }

    public class GetUserByIdQuery : IHaveIdMessage
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdResponse : IMessage
    {
        public User User { get; set; }
    }
}
