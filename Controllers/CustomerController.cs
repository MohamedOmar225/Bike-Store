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
    public class CustomerController : ControllerBase
    {

        private readonly AppDbContext appDbContext;
        public CustomerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new customer
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                appDbContext.Customers.Add(customer);
                appDbContext.SaveChanges();
                return Ok(new
                {
                    Massage = "Customer added Successfully.",
                    Customer = customer
                });
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the customer.");
            }
        }



        // return data
        [HttpGet]
        [Route("All Customers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customer = appDbContext.Customers.Where(c => c.IsActive).ToList();
                //var customer = appDbContext.Customers.ToList();
                if (customer == null)
                {
                    return NotFound("No customers found.");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the customers: {ex.Message}");
            }

        }



        // return data by id
        [HttpGet("Get Customer Information/{id:int}")]
        public IActionResult GetCustomerByID([FromRoute] int id)
        {
            try
            {
                var customer = appDbContext.Customers
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.CustomerId == id && p.IsActive);

                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound($"No customers found with this id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpGet("Get Order Details For Customer id")]
        public IActionResult GetCustomerOrderDetails(int id)
        {
            try
            {
                var customer = appDbContext.Customers
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.CustomerId == id && p.IsActive);

                if (customer != null)
                {
                    var orderdetails = customer.Orders
                        .Where(o => o.IsExist)
                        .Select(o => new OrderShortDto
                        {
                            OrderId = o.OrderId,
                            OrderDate = o.OrderDate,
                            ShippedDate = o.ShippedDate,
                            TotalAmount = o.TotalAmount,
                        }).ToList();                

                    if(!customer.Orders.Any(o => o.IsExist)) // لو مفيش اي اوردرات للعميل دا قيمتها ترو ينفذ
                    {
                        return Ok( new 
                        {
                            CustomerName = customer.CustumerName,
                            massage = "This customer does not make any orders."
                        });
                    }

                    var CustomerDto = new CustomerWithOrdersDto
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustumerName,
                        Orders = orderdetails
                    };
                    return Ok(CustomerDto);
                }                               
                return NotFound("No customer found with this ID");                                               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpGet("{name}")]
        public IActionResult GetCustomerByname([FromRoute] string name)
        {
            try
            {
                var customer = appDbContext.Customers
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.CustumerName.ToLower() == name.ToLower() && p.IsActive);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound($"No customers found with this name.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpGet("Get Order Details For Customer/{name}")]
        public IActionResult GetCustomerOrderDetails(string name)
        {
            try
            {
                var customer = appDbContext.Customers
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.CustumerName == name && p.IsActive);

                if (customer != null)
                {
                    var orderdetails = customer.Orders
                        .Where(o => o.IsExist)
                        .Select(o => new OrderShortDto
                        {
                            OrderId = o.OrderId,
                            OrderDate = o.OrderDate,
                            ShippedDate = o.ShippedDate,
                            TotalAmount = o.TotalAmount,
                        }).ToList();

                    if (!customer.Orders.Any(o => o.IsExist)) // لو مفيش اي اوردرات للعميل دا قيمتها ترو ينفذ
                    {
                        return Ok(new
                        {
                            CustomerName = customer.CustumerName,
                            massage = "This customer does not make any orders."
                        });
                    }

                    var CustomerDto = new CustomerWithOrdersDto
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustumerName,
                        Orders = orderdetails
                    };
                    return Ok(CustomerDto);
                }
                return NotFound("No customer found with this ID");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_customer = appDbContext.Customers                    
                    .FirstOrDefault(p => p.CustomerId == id);
                if (old_customer != null)
                {
                    old_customer.CustumerName = customer.CustumerName;                    
                    old_customer.PhoneNumber = customer.PhoneNumber;
                    old_customer.CustomerEmail = customer.CustomerEmail;
                    old_customer.City = customer.City;
                    old_customer.Street = customer.Street;           
                    old_customer.IsActive = customer.IsActive;
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Customer updated Successfully.",
                        customer = old_customer,
                    });
                }
                return NotFound($"No customers found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = appDbContext.Customers.FirstOrDefault(p => p.CustomerId == id && p.IsActive);
                if (customer != null)
                {
                    customer.IsActive = false;
                    //appDbContext.Customers.Remove(customer);
                    appDbContext.SaveChanges();
                    return Ok("Customer deleted successfully.");
                }
                return NotFound($"No customers found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        //***************************************************

        //[HttpGet("Get Order Details For Customer By Id")]
        //public IActionResult GetCustomerOrderDetails(int id)
        //{
        //    try
        //    {
        //        Customer? customer = appDbContext.Customers
        //            .Include(o => o.Orders)
        //            .FirstOrDefault(p => p.CustomerId == id && p.IsActive);

        //        if (customer != null)
        //        {
        //            var customerdto = new CustomerWithOrdersDto
        //            {
        //                CustomerId = customer.CustomerId,
        //                CustomerName = customer.CustumerName,                       
        //                Orders = customer.Orders.Select(o => new OrderShortDto
        //                {
        //                    OrderId = o.OrderId,
        //                    OrderDate = o.OrderDate,
        //                    ShippedDate = o.ShippedDate,
        //                    TotalAmount = o.TotalAmount,
        //                }).ToList()
        //            };
        //            return Ok(customerdto);
        //        }
        //        else
        //        {
        //            return NotFound($"No customers found with this id.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



        








    }
}
