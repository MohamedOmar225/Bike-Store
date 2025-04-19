namespace bike_store_2.DTO
{
    public class CustomerWithOrdersDto
    {
        

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;        
        public List<OrderShortDto> Orders { get; set; } = new();
    }

    public class OrderShortDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }    
        public DateTime ShippedDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
