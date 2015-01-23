using System.Collections.Generic;
using System.Linq;
using ECommerceFX.Data;

namespace ECommerceFX.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public IEnumerable<Product> ByName(string name)
        {
            return AllByKey(p => p.Name.Contains(name)).ToList();
        }

        public IEnumerable<Product> ByDescription(string description)
        {
            return AllByKey(p => p.Description.Contains(description));
        }
    }
}