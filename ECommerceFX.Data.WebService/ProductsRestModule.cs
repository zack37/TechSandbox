using System;
using System.Collections.Generic;
using ECommerceFX.Data.Messages;
using ECommerceFX.Repository;
using Nancy;
using Nancy.ModelBinding;
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
}