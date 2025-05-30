using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO
{
    public class CustomerWithOrdersDto
    {

        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? CustomerEmail { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public bool IsActive { get; set; } 
        public List<OrderShortDto> Orders { get; set; } = new();
    }

    public class OrderShortDto
    {
        public int StoreId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }    
        public DateTime ShippedDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }




    public class CreateCustomerDTO
    {
        [Required]
        public string CustumerName { get; set; } = null!;
        [Required]
        public string? PhoneNumber { get; set; }         
        [Required]
        public string? CustomerEmail { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Street { get; set; } 
        
    }




    public class UpdateCustomerDto
    {
        [Required]
        public string CustumerName { get; set; } = null!;
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? CustomerEmail { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }









}
