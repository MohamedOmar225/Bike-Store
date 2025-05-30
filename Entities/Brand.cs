using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; } = null!;
        public bool IsExist { get; set; } = true;
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
        
    }
}
