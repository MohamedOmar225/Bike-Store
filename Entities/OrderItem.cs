using bike_store_2.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class OrderItem
    {                
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }         
        public int Quantity { get; set; }
        public decimal Listprice { get; set; }
        public decimal Discount { get; set; }        
        [JsonIgnore]
        public Order? Orders { get; set; } 
        [JsonIgnore]
        public Product? Products { get; set; }
    }
}
