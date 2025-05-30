using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class ImageForProduct
    {
        [Key]
        public int Id { get; set; }
        public string? ImageUrl { get; set; } 
        public string? ImageName { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public bool IsMainImage { get; set; } = false;
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
