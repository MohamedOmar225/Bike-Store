using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bike_store_2.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MinLength(3)]
        public string EmployeeName { get; set; } = null!;
        public string? EmployeeEmail { get; set; }
        public string? EmployeePhone { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal EmployeeSalary { get; set; }
        [ForeignKey("Store")]
        public int? StoreId { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public Store? Store { get; set; } 
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
