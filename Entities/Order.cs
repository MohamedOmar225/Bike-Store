using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }        
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime ShippedDate { get; set; }        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalAmount { get; set; }
        public bool IsExist { get; set; } = true;
        [JsonIgnore]
        public Store? Store { get; set; } 
        [JsonIgnore]
        public Employee? Employee { get; set; } 
        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
