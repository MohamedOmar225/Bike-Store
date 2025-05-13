using System.Reflection.Metadata.Ecma335;

namespace bike_store_2.DTO
{
    public class Store_and_QuantityofProduct_In_It
    {        
        public string StoreName { get; set; }
        //public string? city { get; set; }
        //public string? street { get; set; }
        //public string? Phone { get; set; }
        //public string? Email { get; set; }
        public List<ProductDetails> ProductDetails { get; set; } = new List<ProductDetails>();
    }





    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

}
