using bike_store_2.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace bike_store_2.Entities
{
    public class OrderItem
    {
        public int Item_id { get; set; }
        public int Order_id { get; set; }
        [ForeignKey("Product")]
        public int product_ID { get; set; }
        public int? Quantity { get; set; }
        public decimal List_price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public Order Orders { get; set; } = new Order();

    }
}
