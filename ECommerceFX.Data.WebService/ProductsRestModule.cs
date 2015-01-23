using System;
using System.Collections.Generic;
using ECommerceFX.Data.Messages;
using ECommerceFX.Data.Messages.Commands;
using ECommerceFX.Data.Messages.Events;
using ECommerceFX.Data.Messages.Queries;
using ECommerceFX.Repository;
using Nancy;
using Nancy.ModelBinding;
using NServiceBus;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace ECommerceFX.Data.WebService
{
    public class ProductsRestModule : NancyModule
    {
        private readonly IProductRepository _productsRepository;

        public ProductsRestModule(IProductRepository productsRepository)
            : base("/products")
        {
            _productsRepository = productsRepository;
            Get["/"] = All;
            Get["/all"] = All;
            Get["/id/{id:guid}"] = p => _productsRepository.ById((Guid)p.Id);
            Post["/create"] = CreateProduct;
            Delete["/delete/{id:guid}"] = DeleteProduct;
        }

        public dynamic All(dynamic parameters)
        {
            return new RestResponse<IEnumerable<Product>>(_productsRepository.All());
        }

        public dynamic ById(dynamic parameters)
        {
            return new RestResponse<Product>(_productsRepository.ById((Guid)parameters.Id));
        }

        public dynamic CreateProduct(dynamic parameters)
        {
            var product = this.Bind<Product>();
            var response = new RestResponse<Product>(_productsRepository.Create(product), HttpStatusCode.Created);
            return response;
        }

        public dynamic DeleteProduct(dynamic parameters)
        {
            var product = this.Bind<Product>();
            _productsRepository.Delete(product.Id);
            return new RestResponse<Product>();
        }
    }

    public class ProductServiceBusHandler : IHandleMessages<GetAllProductsQuery>,
                                            IHandleMessages<GetProductByIdQuery>,
                                            IHandleMessages<CreateProduct>,
                                            IHandleMessages<DeleteProduct>
    {
        private readonly IBus _bus;
        private readonly IProductRepository _productsRepository;

        public ProductServiceBusHandler(IBus bus, IProductRepository productsRepository)
        {
            _bus = bus;
            _productsRepository = productsRepository;
        }

        public void Handle(GetAllProductsQuery message)
        {
            var response = new GetAllProductsResponse
            {
                Products = _productsRepository.All()
            };
            _bus.Reply(response);
        }

        public void Handle(GetProductByIdQuery message)
        {
            var response = new GetProductByIdResponse
            {
                Product = _productsRepository.ById(message.Id)
            };
            _bus.Reply(response);
        }

        // TODO: Replace with _bus.Publish(ProductCreated) when I get SignalR working
        public void Handle(CreateProduct message)
        {
            var response = new ProductCreated
            {
                Product = _productsRepository.Create(message.Product)
            };
            _bus.Publish(response);
        }

        public void Handle(DeleteProduct message)
        {
            _productsRepository.Delete(message.Id);
            _bus.Publish<ProductDeleted>(x => x.Id = message.Id);
        }
    }
}