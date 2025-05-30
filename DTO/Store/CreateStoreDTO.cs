using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO.Store
{
    public class CreateStoreDTO
    {
        [Required]
        public string StoreName { get; set; } = null!;
        [Required]
        public string? city { get; set; }
        [Required]
        public string? street { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
       
    }



    public class UpdateStoreDto
    {
        [Required]
        public string StoreName { get; set; } = null!;
        [Required]
        public string? city { get; set; }
        [Required]
        public string? street { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public bool IsExist { get; set; }
    }



    public class StoreWithOrdersDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string? city { get; set; }
        public string? street { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsExist { get; set; }
        public List<OrderDetailsDto> Orders { get; set; } = null!;
    }





}
