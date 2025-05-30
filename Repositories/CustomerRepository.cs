using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Repositories
{




    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerWithOrdersDto>> GetAllExistingCustomersAsync();
        Task<IEnumerable<CustomerWithOrdersDto>> GetAllDeletedCustomersAsync();
        Task<CustomerWithOrdersDto?> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<CustomerWithOrdersDto?>> GetCustomerByNameAsync(string customerName);
        Task<Customer> CreateCustomerAsync(CreateCustomerDTO createCustomer);
        Task<Customer> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomer);        
        Task DeleteCustomerAsync(int id);
    }








    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDbContext _appDbContext;
        public CustomerRepository(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }






        public async Task<Customer> CreateCustomerAsync(CreateCustomerDTO createCustomer)
        {
            var customer = new Customer
            {
                CustumerName = createCustomer.CustumerName,
                PhoneNumber = createCustomer.PhoneNumber,
                CustomerEmail = createCustomer.CustomerEmail,
                City = createCustomer.City,
                Street = createCustomer.Street,
                IsActive = true
            };
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();

            return customer;
        }



        public async Task<Customer> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomer)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id && c.IsActive);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            customer.CustumerName = updateCustomer.CustumerName;
            customer.PhoneNumber = updateCustomer.PhoneNumber;
            customer.CustomerEmail = updateCustomer.CustomerEmail;
            customer.City = updateCustomer.City;
            customer.Street = updateCustomer.Street;
            customer.IsActive = updateCustomer.IsActive;
            _appDbContext.Customers.Update(customer);
            await _appDbContext.SaveChangesAsync();

            return customer;
        }


       

        public async Task<IEnumerable<CustomerWithOrdersDto>> GetAllExistingCustomersAsync()
        {
            var customers = await _appDbContext.Customers
                .Include(c => c.Orders)
                .Where(c => c.IsActive)
                .ToListAsync();

            var result = new List<CustomerWithOrdersDto>();
            foreach (var customer in customers)
            {
                var order = await _appDbContext.Orders.Select(o => new OrderShortDto
                {
                    StoreId = o.StoreId,
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    TotalAmount = o.TotalAmount
                }).ToListAsync();

                var customerDetails = new CustomerWithOrdersDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustumerName,
                    PhoneNumber = customer.PhoneNumber,
                    CustomerEmail = customer.CustomerEmail,
                    City = customer.City,
                    Street = customer.Street,
                    IsActive = true,
                    Orders = order
                };
                result.Add(customerDetails);
            }
            return result;
        }




        public async Task<IEnumerable<CustomerWithOrdersDto>> GetAllDeletedCustomersAsync()
        {
            var customers = await _appDbContext.Customers
                            .Include(c => c.Orders)
                            .Where(c => !c.IsActive)
                            .ToListAsync();

            var result = new List<CustomerWithOrdersDto>();
            foreach (var customer in customers)
            {
                var order = await _appDbContext.Orders.Select(o => new OrderShortDto
                {
                    StoreId = o.StoreId,
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    TotalAmount = o.TotalAmount
                }).ToListAsync();

                var customerDetails = new CustomerWithOrdersDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustumerName,
                    PhoneNumber = customer.PhoneNumber,
                    CustomerEmail = customer.CustomerEmail,
                    City = customer.City,
                    Street = customer.Street,
                    IsActive = true,
                    Orders = order
                };
                result.Add(customerDetails);
            }
            return result;
        }



        public async Task<CustomerWithOrdersDto?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _appDbContext.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.IsActive);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {customerId} not found.");

            var orders = await _appDbContext.Orders.Select(o => new OrderShortDto
            {
                StoreId = o.StoreId,
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                ShippedDate = o.ShippedDate,
                TotalAmount = o.TotalAmount
            }).ToListAsync();
           
            var customerDetails = new CustomerWithOrdersDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustumerName,
                PhoneNumber = customer.PhoneNumber,
                CustomerEmail = customer.CustomerEmail,
                City = customer.City,
                Street = customer.Street,
                IsActive = true,
                Orders = orders
            };
            return customerDetails;

        }



        public async Task<IEnumerable<CustomerWithOrdersDto?>> GetCustomerByNameAsync(string customerName)
        {
            var customers = await _appDbContext.Customers
                .Where(c => c.CustumerName.ToLower() == customerName.ToLower() && c.IsActive).Include(c => c.Orders)
                .ToListAsync();

            if (customers == null || !customers.Any())
                throw new KeyNotFoundException($"Customer with name {customerName} not found.");

            var orders = customers.SelectMany(c => c.Orders)
                .Select(o => new OrderShortDto
                {
                    StoreId = o.StoreId,
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    TotalAmount = o.TotalAmount
                }).ToList();           

            var result = new List<CustomerWithOrdersDto>();
            foreach ( var customer in customers)
            {
                var customerDetails = new CustomerWithOrdersDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustumerName,
                    PhoneNumber = customer.PhoneNumber,
                    CustomerEmail = customer.CustomerEmail,
                    City = customer.City,
                    Street = customer.Street,
                    IsActive = true,
                    Orders = orders
                };                
                result.Add(customerDetails);
            }

            return result;
        }





        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id && c.IsActive);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            customer.IsActive = false;
            _appDbContext.Customers.Update(customer);
            await _appDbContext.SaveChangesAsync();

            var orders = await _appDbContext.Orders.Where(o => o.CustomerId == id && o.IsExist).ToListAsync();
            if (orders != null && orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    order.IsExist = false;
                    _appDbContext.Orders.Update(order);
                }
            }
            await _appDbContext.SaveChangesAsync();

        }



    }
}
