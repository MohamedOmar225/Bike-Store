using bike_store_2.Entities;
using System.Text.Json.Serialization;

namespace bike_store_2.DTO
{
    public class List_all_products__for__specific_categoryDTO
    {
        public string Category_Name { get; set; }
        public List<string> Product_Name { get; set; } = new List<string>();
        //[JsonIgnore]
        public virtual List<Product>? Products { get; set; } = new List<Product>();
    }
}
