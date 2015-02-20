using System;
using System.Collections.Generic;
using ECommerceFX.Data;

namespace ECommerceFX.Web.Services
{
    public interface IProductDataService : IDataService<Product>
    {
        IEnumerable<Product> All();
        Product ById(Guid id);
        Product Create(Product product);
        void Delete(Guid id);
        Product Update(Product product);
        IEnumerable<Product> ByName(string name);
        IEnumerable<Product> ByDescription(string description);
    }
}