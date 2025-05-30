using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bike_store_2.DTO
{
    public class EmployeeMakedOrdersDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string? EmployeeEmail { get; set; }
        [Required]
        public string? EmployeePhone { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
        [Required]
        public int? StoreId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public List<OrderShortDto> Orders { get; set; } = new List<OrderShortDto>();
    }




    public class CreateEmployeeDTO
    {
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string? EmployeeEmail { get; set; }
        [Required]
        public string? EmployeePhone { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
        [Required]
        public int? StoreId { get; set; }

    }





    public class UpdateEmployeeDTO
    {
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string? EmployeeEmail { get; set; }
        [Required]
        public string? EmployeePhone { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
        [Required]
        public int? StoreId { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

    }


}
