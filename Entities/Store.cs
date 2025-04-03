using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Store
    {
        [Key]
        public int store_id { get; set; }
        [Required]
        public string store_name { get; set; } = null!;
        public string? city { get; set; }
        public string? street { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
