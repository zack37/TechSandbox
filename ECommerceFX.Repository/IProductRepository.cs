using System.Collections.Generic;
using ECommerceFX.Data;

namespace ECommerceFX.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> ByName(string name);
        IEnumerable<Product> ByDescription(string description);
    }
}