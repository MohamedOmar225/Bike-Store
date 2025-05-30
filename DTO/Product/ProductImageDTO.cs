using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace bike_store_2.DTO
{
    public class ProductImageDTO
    {      
        public int ProductId { get; set; }        
        public string? ImageUrl { get; set; }
        public bool IsMainImage { get; set; } = false;
        public IFormFile? ImageFile { get; set; }
    }




    public class UpdateProductImageDTO
    {
        public int ProductId { get; set; }
        public bool IsMainImage { get; set; } = false;
        public IFormFile? ImageFile { get; set; }
    }


  

}
