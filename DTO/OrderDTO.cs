using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bike_store_2.DTO
{
    public class OrderDto // post / put
    {
        public int CustomerId { get; set; }        
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }        
        public DateTime ShippedDate { get; set; }
        public bool IsExist { get; set; } = true;
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }




    public class OrderItemDto
    {
        public int ProductId { get; set; }           
        public decimal Listprice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }        
    }





    public class OrderDetailsDto // get 
    {
        public int OrderId { get; set; }
        public int CustomertId { get; set; }        
        public string EmployeeName { get; set; } 
        public string StoreName { get; set; } 
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }        
        public decimal? TotalAmount { get; set; }
        //[JsonIgnore]
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }




    

}
