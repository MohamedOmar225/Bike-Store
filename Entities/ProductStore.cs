using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bike_store_2.Entities
{
    public class ProductStore
    {       
        public int ProductId { get; set; }        
        public int StoreId { get; set; }
        public int Quanttity { get; set; }
        [JsonIgnore]
        public Product? Products { get; set; } 
        [JsonIgnore]
        public Store? Stores { get; set; } 
    }
}
