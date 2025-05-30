using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;        
        public bool IsExsit { get; set; } = true;
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();                
    }
}
