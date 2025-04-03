using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Customer
    {
        [Key]
        public int customer_Id { get; set; }
        [Required]
        public string First_Name { get; set; } = null!;
        [Required]
        public string Last_Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string? City { get; set; }
        public string? Street { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}