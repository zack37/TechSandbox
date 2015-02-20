using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceFX.Data;
using ECommerceFX.Data.Messages.Commands;
using ECommerceFX.Data.Messages.Queries;
using NServiceBus;

namespace ECommerceFX.Web.Services
{
    public class ProductDataService : DataService<Product>, IProductDataService
    { 
        public ProductDataService(IBus bus)
            :base(bus) { }

        public IEnumerable<Product> All()
        {
            return All<GetAllProductsQuery, GetAllProductsResponse>(response => response.Products);
        }

        public Product ById(Guid id)
        {
            return ById<GetProductByIdQuery, GetProductByIdResponse>(id, response => response.Product);
        }

        public Product Create(Product product)
        {
            return Create<CreateProduct>(product, command => command.Product = product);
        }

        public void Delete(Guid id)
        {
            Delete<DeleteProduct>(id, command => command.Id = id);
        }

        public Product Update(Product product)
        {
            return Update<UpdateProduct>(product, command => command.Product = product);
        }

        public IEnumerable<Product> ByName(string name)
        {
            return SendRequest<GetAllProductsQuery, GetAllProductsResponse>().Products.Where(x => x.Name == name);
        }

        public IEnumerable<Product> ByDescription(string description)
        {
            return SendRequest<GetAllProductsQuery, GetAllProductsResponse>() .Products.Where(x => x.Description == description);
        }
    }
}