using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceFX.Data;

namespace ECommerceFX.Web.Services
{
    public interface IProductService : IService<Product>
    {
        IEnumerable<Product> ByName(string name);
        Task<IEnumerable<Product>> ByNameAsync(string name);
        IEnumerable<Product> ByDescription(string description);
        Task<IEnumerable<Product>> ByDescriptionAsync(string description);
    }
}