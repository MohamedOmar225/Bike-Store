namespace bike_store_2.DTO
{
    public class BrandWithCategoriesDto
    {
        public string BrandName { get; set; }
        public List<ProductDTO> ProductDTO { get; set; } = new List<ProductDTO>();
    }





    public class ProductDTO
    {
        public string ProductName { get; set; }
    }

}
