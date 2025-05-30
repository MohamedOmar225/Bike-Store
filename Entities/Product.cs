using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{    
    public class Product
{       
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        public string ModelYear { get; set; } = null!;
        [Range(1000, 100000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ListPrice { get; set; }
        [ForeignKey("Brands")]
        public int? BrandId { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public bool IsExisit { get; set; } = true;        
        [JsonIgnore]
        public Brand? Brands { get; set; } 
        [JsonIgnore]
        public Category? Category { get; set; } 
        [JsonIgnore]
        public ICollection<Store> Stores { get; set; } = new List<Store>();
        [JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        [JsonIgnore]
        public ICollection<ImageForProduct> imageForProducts { get; set; } = new List<ImageForProduct>();
    }
}
