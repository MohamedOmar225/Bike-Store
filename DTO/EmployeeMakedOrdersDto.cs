namespace bike_store_2.DTO
{
    public class EmployeeMakedOrdersDto
    {        
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<EmployeeOrderShortDto> employeeOrderShortDtos { get; set; } = new List<EmployeeOrderShortDto>();
    }



    public class EmployeeOrderShortDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
