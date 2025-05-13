using bike_store_2.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustumerName { get; set; } = null!;        
        public string? PhoneNumber { get; set; }
        public string? CustomerEmail { get; set; } 
        public string? City { get; set; }
        public string? Street { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();        
    }
}