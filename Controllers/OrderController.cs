using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        private readonly AppDbContext appDbContext;
        public OrderController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }




        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employee = appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == orderDto.EmployeeId && e.IsActive);
                var customer = appDbContext.Customers.FirstOrDefault(e => e.CustomerId == orderDto.CustomerId && e.IsActive);
                var store = appDbContext.Stores.FirstOrDefault(e => e.StoreId == orderDto.StoreId && e.IsExist);
                
                if (employee != null && customer != null && store != null)
                {
                    if (employee.StoreId != store.StoreId) // اتاكد ان الموظف شغال ف  المحل الي اتعمل منه الاوردر
                        return BadRequest("The employee does not work at this store.");                   

                    foreach (var item in orderDto.Items)
                    {
                        // بتاكد ان المنتج موجود في جدول المنتجات ويكون true
                        var product = appDbContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId && p.IsExisit);
                        if (product == null)
                            return BadRequest("Product not found.");
                        // بتاكد ان المنتج موجود في المحل
                        var quantityinstore = appDbContext.ProductStores
                           .FirstOrDefault(ps => ps.ProductId == item.ProductId && ps.StoreId == orderDto.StoreId);
                        if (quantityinstore == null)
                            return BadRequest("The product is not available in this store.");

                        // بتاكد ان الكميه المطلوبه اقل من الاموجوده في المحل
                        if(quantityinstore.Quanttity < item.Quantity)
                            return BadRequest("The available quantity of the product is less than the required quantity");
                    }
                    
                    // بعمل الاوردر
                    var order = new Order
                    {
                        CustomerId = orderDto.CustomerId,                        
                        EmployeeId = orderDto.EmployeeId,
                        StoreId = orderDto.StoreId,
                        OrderDate = DateTime.UtcNow.ToLocalTime(),
                        ShippedDate = orderDto.ShippedDate.ToLocalTime(),
                        OrderItems = orderDto.Items.Select(oi => new OrderItem
                        {
                            ProductId = oi.ProductId,                            
                            Listprice = oi.Listprice,
                            Quantity = oi.Quantity,
                            Discount = oi.Discount,
                        }).ToList(),
                    };

                    order.TotalAmount = order.OrderItems.Sum(i => i.Quantity * i.Listprice * (1 - i.Discount / 100));

                    // بخصم المنتجات الي اتطلب من المحل
                    foreach (var item in order.OrderItems)
                    {
                        var productisstore = appDbContext.ProductStores
                            .FirstOrDefault(ps => ps.ProductId == item.ProductId && ps.StoreId == order.StoreId);

                        if (productisstore != null)
                            productisstore.Quanttity -= item.Quantity;
                    } 
                    appDbContext.Orders.Add(order);
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Order created successfully.",
                        order = order,
                        orderitems = orderDto.Items.Select(i => new
                        {
                            i.ProductId,
                            i.Listprice,
                            i.Quantity,
                            i.Discount,
                        }).ToList()
                    });                                                                                                    
                }
                return BadRequest("Check employee, customer, store if thay are exist or not.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("All Orders")]
        public IActionResult GetAllOrders()
        {            
            try
            {
                var orders = appDbContext.Orders
               .Include(o => o.Customer)
               .Include(o => o.Employee)
               .Include(o => o.Store)
               .Include(o => o.OrderItems)
               .Where(o => o.IsExist)
               .Select(o => new OrderDetailsDto
               {
                   OrderId = o.OrderId,                   
                   CustomertId = o.CustomerId,
                   EmployeeName = o.Employee.EmployeeName,
                   StoreName = o.Store.StoreName,
                   OrderDate = o.OrderDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                   ShippedDate = o.ShippedDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                   TotalAmount = o.TotalAmount,
                   Items = o.OrderItems.Select(oi => new OrderItemDto
                   {
                       ProductId = oi.ProductId,                       
                       Listprice = oi.Listprice,
                       Quantity = oi.Quantity,
                       Discount = oi.Discount,
                   }).ToList()
               }).ToList();

                if (!orders.Any())
                {
                    return NotFound("No orders found.");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("{id:int}")]
        public IActionResult GetOrderByID([FromRoute] int id)
        {
            try
            {
                var order = appDbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Store)
                .Include(o => o.OrderItems)                
                .FirstOrDefault(o => o.OrderId == id && o.IsExist);
                if (order != null)
                {
                    var orderdto = new OrderDetailsDto
                    {
                        OrderId = order.OrderId,                        
                        CustomertId = order.Customer.CustomerId,
                        EmployeeName = order.Employee.EmployeeName,
                        StoreName = order.Store.StoreName,
                        OrderDate = order.OrderDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ShippedDate = order.ShippedDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        TotalAmount = order.TotalAmount,
                        Items = order.OrderItems.Select(o => new OrderItemDto
                        {
                            ProductId = o.ProductId,                            
                            Listprice = o.Listprice,
                            Quantity = o.Quantity,
                            Discount = o.Discount,
                        }).ToList()
                    };
                    return Ok(orderdto);
                }
                return NotFound($"No orders found with this id.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }        


        [HttpPut("{id:int}")]
        public IActionResult UpdateOrder([FromRoute] int  id , [FromBody] OrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == orderDto.EmployeeId && e.IsActive);
                var customer = appDbContext.Customers.FirstOrDefault(e => e.CustomerId == orderDto.CustomerId && e.IsActive);
                var store = appDbContext.Stores.FirstOrDefault(e => e.StoreId == orderDto.StoreId && e.IsExist);

                if (employee != null && customer != null && store != null)
                {
                    if (employee.StoreId != store.StoreId) 
                        return BadRequest("The employee does not work at this store.");

                    foreach (var item in orderDto.Items)
                    {                        
                        var product = appDbContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId && p.IsExisit);
                        if (product == null)
                            return BadRequest("Product not found.");        
                        
                        var quantityinstore = appDbContext.ProductStores
                           .FirstOrDefault(ps => ps.ProductId == item.ProductId && ps.StoreId == orderDto.StoreId);
                        if (quantityinstore == null)
                            return BadRequest("The product is not available in this store.");
                       
                        if (quantityinstore.Quanttity < item.Quantity)
                            return BadRequest("The available quantity of the product is less than the required quantity");
                    }

                    var order = appDbContext.Orders.Include(oi => oi.OrderItems)
                    .FirstOrDefault(o => o.OrderId == id);
                    if (order != null)
                    {
                        order.CustomerId = orderDto.CustomerId;
                        order.EmployeeId = orderDto.EmployeeId;
                        order.StoreId = orderDto.StoreId;                        
                        order.ShippedDate = orderDto.ShippedDate.ToLocalTime();
                        order.IsExist = orderDto.IsExist;

                        order.OrderItems = orderDto.Items.Select(o => new OrderItem
                        {
                            ProductId = o.ProductId,
                            Listprice = o.Listprice,
                            Quantity = o.Quantity,
                            Discount = o.Discount,

                        }).ToList();

                        order.TotalAmount = orderDto.Items.Sum(o => o.Listprice * o.Quantity * (1 - o.Discount / 100));
                        
                        foreach (var item in order.OrderItems)
                        {
                            var productisstore = appDbContext.ProductStores
                                .FirstOrDefault(ps => ps.ProductId == item.ProductId && ps.StoreId == order.StoreId);

                            if (productisstore != null)
                                productisstore.Quanttity -= item.Quantity;
                        }

                        appDbContext.SaveChanges();
                        return Ok(new
                        {
                            Massage = "Order updated successfully.",
                            order = order,
                            orderitems = orderDto.Items.Select(i => new
                            {
                                i.ProductId,
                                i.Listprice,
                                i.Quantity,
                                i.Discount,
                            }).ToList()
                        });
                    }
                    return NotFound($"No orders found with this id.");
                }
                return BadRequest("Check employee, customer, store if thay are exist or not.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }




        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var order = appDbContext.Orders.FirstOrDefault(o => o.OrderId == id && o.IsExist);
                if(order != null)
                {
                    order.IsExist = false;                    
                    appDbContext.SaveChanges();
                    return Ok("Oredr deleted succesfully.");
                }
                return NotFound($"No orders found with this id.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }














    }
}
