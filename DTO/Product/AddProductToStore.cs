using bike_store_2.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace bike_store_2.DTO
{
    public class AddProductToStore
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ModelYear { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
        //[ModelBinder(BinderType = typeof(JsonModelBinder))]
        public List<StoerQuantity> stoerQuantities { get; set; } = new List<StoerQuantity>();
    }



    public class StoerQuantity
    {       
        public int StoreId { get; set; }
        public int Quantity { get; set; }
    }





    public class AddProductToStoreDto
    {
        
        //public int ProductId { get; set; }
        public List<StoerQuantity> stoerQuantities { get; set; } = new List<StoerQuantity>();
    }



    public class ProductDetailsdto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsExist { get; set; }
        public List<string> ImageURLs { get; set; } = new List<string>();
        public List<StoerQuantity> Stores { get; set; } = new List<StoerQuantity>();
    }





    public class UpdateProductDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ModelYear { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public bool IsExist { get; set; }
        [Required]
        public int MainImage { get; set; }
        public List<StoerQuantity> Stores { get; set; } = new List<StoerQuantity>();
    }





    public class UploadeImagesForProducts
    {
        [Required]
        public int productId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public List<IFormFile> ImageFile { get; set; } = new List<IFormFile>();
    }



    
}
