using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bike_store_2.Entities
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }
        [ForeignKey("Customer")]
        public int customer_id { get; set; }       
        private DateTime order_date;
        public DateTime OrderDate
        {
            get => order_date.Date; // يعيد فقط الجزء الخاص بالتاريخ
            set => order_date = value.Date; // يحفظ فقط التاريخ بدون وقت
        }
        private DateTime shipped_date;
        public DateTime ShippedDate
        {
            get => order_date.Date; // يعيد فقط الجزء الخاص بالتاريخ
            set => order_date = value.Date; // يحفظ فقط التاريخ بدون وقت
        }       
        [ForeignKey("Employee")]
        public int Emp_id { get; set; }
        [ForeignKey("Store")]
        public int Store_id { get; set; }
        public Store Store { get; set; } = new Store();
        public Employee Employee { get; set; } = new Employee();
        public Customer Customer { get; set; } = new Customer();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
