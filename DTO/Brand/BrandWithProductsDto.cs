using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO
{
    public class BrandWithProductsDto
    {
        public string BrandName { get; set; }
        public List<ProductDTO> ProductDTO { get; set; } = new List<ProductDTO>();
    }





    public class ProductDTO
    {
        public string ProductName { get; set; }
        public decimal price { get; set; }
        public string ModelYear { get; set; }
    }


    public class StoreDTO
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }



    public class CreateBrandDto
    {
        [Required]
        public string BrandName { get; set; } = null!;
    }



    public class UpdateBrandDto
    {
        [Required]
        public string BrandName { get; set; }
        [Required]
        public bool IsExist { get; set; }
    }

    public class BrandDetailsDTO
    {
        [Required]
        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public bool IsExist { get; set; }
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public List<StoreDTO> Stores { get; set; } = new List<StoreDTO>();
    }



}
