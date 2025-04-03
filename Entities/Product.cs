using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{    
    public class Product
{       
        [Key]
        public int product_id { get; set; }
        [Required]
        public string product_name { get; set; } = null!;
        public string? model_year { get; set; }
        [Range(1000, 100000)]
        public decimal? list_price { get; set; }
        [ForeignKey("Brands")]
        public int? brand_id { get; set; }
        [ForeignKey("Category")]
        public int? cate_id { get; set; }
        //[JsonIgnore]
        public Brand Brands { get; set; } = new Brand();
        [JsonIgnore]
        public Category Category { get; set; } = new Category();
        [JsonIgnore]
        public ICollection<Store> Stores { get; set; } = new List<Store>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
