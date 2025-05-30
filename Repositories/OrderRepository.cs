using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;
namespace bike_store_2.Repositories
{



    public interface IOrderRepository
    {      
        Task<IEnumerable<OrderDetailsDto>> GetAllExistingOrdersAsync();
        Task<IEnumerable<OrderDetailsDto>> GetAllDeletedOrdersAsync();
        Task<OrderDetailsDto?> GetOrderByIdAsync(int orderId);        
        Task<Order> CreateOrderAsync(OrderDto createOrder);
        Task<Order> UpdateOrderAsync(int orderId, OrderDto updateOrder);
        Task DeleteOrderAsync(int orderId);
    }





    public class OrderRepository : IOrderRepository
    {

        public readonly AppDbContext _appDbContext;
        public OrderRepository(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }



        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {            

            var employee =await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == orderDto.EmployeeId && e.IsActive);
            var customer =await _appDbContext.Customers.FirstOrDefaultAsync(e => e.CustomerId == orderDto.CustomerId && e.IsActive);
            var store =await _appDbContext.Stores.FirstOrDefaultAsync(e => e.StoreId == orderDto.StoreId && e.IsExist);

            if (employee != null && customer != null && store != null)
            {
                if (employee.StoreId != store.StoreId) // اتاكد ان الموظف شغال ف  المحل الي اتعمل منه الاوردر
                    throw new Exception("Employee and Store do not match");


                foreach (var item in orderDto.Items)
                {
                    // بتاكد ان المنتج موجود في جدول المنتجات ويكون true
                    var product =await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId && p.IsExisit);
                    if (product == null)
                        throw new Exception($"Product with ID {item.ProductId} not found");
                    // بتاكد ان المنتج موجود في المحل
                    var quantityinstore =await _appDbContext.ProductStores
                    .FirstOrDefaultAsync(ps => ps.ProductId == item.ProductId && ps.StoreId == orderDto.StoreId);

                    if (quantityinstore == null)
                        throw new Exception($"Product with ID {item.ProductId} not found in store {orderDto.StoreId}");


                    // بتاكد ان الكميه المطلوبه اقل من الاموجوده في المحل
                    if (quantityinstore.Quanttity < item.Quantity)
                        throw new Exception($"Product with ID {item.ProductId} not enough in store {orderDto.StoreId}");

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
                if(order.TotalAmount < 0 || order.TotalAmount == 0)
                    throw new Exception("LIST PRICE, QUANTITY, and DISCONT cannot be zero or negative");

                // بخصم المنتجات الي اتطلب من المحل
                foreach (var item in order.OrderItems)
                {
                    var productisstore = _appDbContext.ProductStores
                        .FirstOrDefault(ps => ps.ProductId == item.ProductId && ps.StoreId == order.StoreId);

                    if (productisstore != null)
                        productisstore.Quanttity -= item.Quantity;
                }
                _appDbContext.Orders.Add(order);
                _appDbContext.SaveChanges();

                return order;
            }
            throw new Exception("Employee, Customer or Store not found");
        }





        public async Task<Order> UpdateOrderAsync(int orderId, OrderDto updateOrder)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == updateOrder.EmployeeId && e.IsActive);
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(e => e.CustomerId == updateOrder.CustomerId && e.IsActive);
            var store = await _appDbContext.Stores.FirstOrDefaultAsync(e => e.StoreId == updateOrder.StoreId && e.IsExist);
            if(employee != null && customer != null && store != null)
            {
                if (employee.StoreId != store.StoreId) // اتاكد ان الموظف شغال ف  المحل الي اتعمل منه الاوردر
                    throw new Exception("Employee and Store do not match");


                foreach (var item in updateOrder.Items)
                {
                    // بتاكد ان المنتج موجود في جدول المنتجات ويكون true
                    var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId && p.IsExisit);
                    if (product == null)
                        throw new Exception($"Product with ID {item.ProductId} not found");
                    // بتاكد ان المنتج موجود في المحل
                    var quantityinstore = await _appDbContext.ProductStores
                    .FirstOrDefaultAsync(ps => ps.ProductId == item.ProductId && ps.StoreId == updateOrder.StoreId);

                    if (quantityinstore == null)
                        throw new Exception($"Product with ID {item.ProductId} not found in store {updateOrder.StoreId}");


                    // بتاكد ان الكميه المطلوبه اقل من الاموجوده في المحل
                    if (quantityinstore.Quanttity < item.Quantity)
                        throw new Exception($"Product with ID {item.ProductId} not enough in store {updateOrder.StoreId}");

                }
                var order = await _appDbContext.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId && o.IsExist);
                
                if (order == null)
                    throw new KeyNotFoundException($"Order with ID {orderId} not found.");

                order.CustomerId = updateOrder.CustomerId;
                order.EmployeeId = updateOrder.EmployeeId;
                order.StoreId = updateOrder.StoreId;
                order.ShippedDate = updateOrder.ShippedDate.ToLocalTime();
                order.IsExist = updateOrder.IsExist;
                order.OrderItems = updateOrder.Items.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Listprice = oi.Listprice,
                    Quantity = oi.Quantity,
                    Discount = oi.Discount,
                }).ToList();

                order.TotalAmount = updateOrder.Items.Sum(i => i.Quantity * i.Listprice * (1 - i.Discount / 100));
                if (order.TotalAmount < 0 || order.TotalAmount == 0)
                    throw new Exception("Total amount cannot be zero or negative");

                // بخصم المنتجات الي اتطلب من المحل
                foreach (var item in order.OrderItems)
                {
                    var productisstore = _appDbContext.ProductStores
                        .FirstOrDefault(ps => ps.ProductId == item.ProductId && ps.StoreId == order.StoreId);

                    if (productisstore != null)
                        productisstore.Quanttity -= item.Quantity;
                }
                _appDbContext.Orders.Update(order);
                await _appDbContext.SaveChangesAsync();
                return order;
            }
            throw new Exception("Employee, Customer or Store not found");
        }






        public async Task<IEnumerable<OrderDetailsDto>> GetAllExistingOrdersAsync()
        {
            var orders = await _appDbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Store)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Products)
                .Where(o => o.IsExist).ToListAsync();

            var result = new List<OrderDetailsDto>();
            foreach (var order in orders)
            {
                var orderDetails = new OrderDetailsDto
                {
                    OrderId = order.OrderId,
                    CustomertId = order.Customer.CustomerId,
                    EmployeeName = order.Employee.EmployeeName,
                    StoreName = order.Store.StoreName,
                    OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
                    ShippedDate = order.ShippedDate.ToString("yyyy-MM-dd"),
                    TotalAmount = order.TotalAmount,
                    Items = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        Listprice = oi.Listprice,
                        Discount = oi.Discount
                    }).ToList()
                };
                result.Add(orderDetails);
            }


          return result;
        }






        public async Task<IEnumerable<OrderDetailsDto>> GetAllDeletedOrdersAsync()
        {
            var orders = await _appDbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Store)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Products)
                .Where(o => !o.IsExist).ToListAsync();

            var result = new List<OrderDetailsDto>();
            foreach (var order in orders)
            {
                var orderDetails = new OrderDetailsDto
                {
                    OrderId = order.OrderId,
                    CustomertId = order.Customer.CustomerId,
                    EmployeeName = order.Employee.EmployeeName,
                    StoreName = order.Store.StoreName,
                    OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
                    ShippedDate = order.ShippedDate.ToString("yyyy-MM-dd"),
                    TotalAmount = order.TotalAmount,
                    Items = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        Listprice = oi.Listprice,
                        Discount = oi.Discount
                    }).ToList()
                };
                result.Add(orderDetails);
            }


            return result;
        }
       





        public async Task<OrderDetailsDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _appDbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Store)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Products)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.IsExist);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            var orderDetails = new OrderDetailsDto
            {
                OrderId = order.OrderId,
                CustomertId = order.Customer.CustomerId,
                EmployeeName = order.Employee.EmployeeName,
                StoreName = order.Store.StoreName,
                OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
                ShippedDate = order.ShippedDate.ToString("yyyy-MM-dd"),
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Listprice = oi.Listprice,
                    Discount = oi.Discount
                }).ToList()
            };
            return orderDetails;
        }
      

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _appDbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.IsExist);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");

            order.IsExist = false;
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync();        
        }


    }
}
