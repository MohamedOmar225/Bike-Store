namespace bike_store_2.DTO
{
    public class GetProductsandCategoryNameDTO
    {
       
        public string Name { get; set; }
        public string? model_year { get; set; }
        public decimal? Price { get; set; }
        public string? category_name { get; set;}

    }
}
