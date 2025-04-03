using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bike_store_2.Entities
{
    public class Employee
    {
        [Key]
        public int Emp_id { get; set; }
        [Required]
        [MinLength(3)]
        public string Emp_name { get; set; } = null!;
        public string? Emp_Email { get; set; }
        public string? Emp_phone { get; set; }
        public decimal Emp_salary { get; set; }
        [ForeignKey("Store")]
        public int? Store_id { get; set; }
        public Store Store { get; set; } = new Store();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
