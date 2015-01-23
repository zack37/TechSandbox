using System.Collections.Generic;
using System.Linq;
using ECommerceFX.Data;

namespace ECommerceFX.Web.ViewModels.Products
{
    public class ProductViewModels
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public ProductViewModels(IEnumerable<Product> products)
        {
            Products = products.Select(p => new ProductViewModel(p));
        }
    }
}