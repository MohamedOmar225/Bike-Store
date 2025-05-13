using bike_store_2.Entities;
using System.Text.Json.Serialization;

namespace bike_store_2.DTO
{
    public class List_all_products__for__specific_categoryDTO
    {
        public string CategoryName { get; set; } = null!;
        public List<ProductDetailsDTO> ProductDetails { get; set; } = new List<ProductDetailsDTO>();
        
    }



    public class ProductDetailsDTO
    {
        public string ProductName { get; set; }
        public decimal price { get; set; }
        public string ModelYear { get; set; }
    }


}
