using System.ComponentModel.DataAnnotations;
using ECommerceFX.Data;
using Newtonsoft.Json;

namespace ECommerceFX.Web.ViewModels.Products
{
    public class ProductViewModel : DatabaseObjectViewModel<Product>
    {
        [Required]
        [StringLength(256)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(1500)]
        [JsonProperty("description")]
        public string Description { get; set; }

        public ProductViewModel() { }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
        }
    }
}