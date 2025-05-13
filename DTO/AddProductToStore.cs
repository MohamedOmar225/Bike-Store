namespace bike_store_2.DTO
{
    public class AddProductToStore
    {
        public string ProductName { get; set; }
        public string ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }      
        
        public List<StoerQuantity> stoerQuantities { get; set; } = new List<StoerQuantity>();
    }



    public class StoerQuantity
    {
        public int StoreId { get; set; }
        public int Quantity { get; set; }
    }


}
