using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO
{
    public class GetCategoryIdandNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }





    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = null!;        
    }


    public class CategoryUpdateDTO
    {
        [Required]
        public string CategoryName { get; set; } 
        [Required]
        public bool IsExsit { get; set; } 
    }


    public class CategoryDetailsDTO
    {
        [Required]
        public int CategoryId { get; set; }
        [Required] 
        public string CategoryName { get; set; }
        [Required]
        public bool IsExsit { get; set; }
        public List<CategoryProductDTO> Products { get; set; } = new List<CategoryProductDTO>();
        public List<CategoryStoreDTO> Stores { get; internal set; }
    }


    public class CategoryProductDTO
    {
        public string ProductName { get; set; }
        public decimal price { get; set; }
        public string ModelYear { get; set; }
    }


    public class  CategoryStoreDTO
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }



}
