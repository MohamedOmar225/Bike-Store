using System.ComponentModel.DataAnnotations;

namespace bike_store_2.Entities
{
    public class ProductStore
    {       
        public int product_id { get; set; }        
        public int store_id { get; set; }
        public int Quanttity { get; set; }

        public Product Products { get; set; } = new Product();
        public Store Stores { get; set; } = new Store();
    }
}
