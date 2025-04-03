using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Brand
    {
        [Key]
        public int brand_id { get; set; }
        [Required]
        public string brand_name { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
