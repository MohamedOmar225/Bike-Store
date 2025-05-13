using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly AppDbContext appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }




        // create new employee
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {           
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var CheckStoreExsist = appDbContext.Stores.FirstOrDefault(s => s.StoreId == employee.StoreId && s.IsExist);
                if (CheckStoreExsist != null)
                {
                    appDbContext.Employees.Add(employee);                   
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Employee added Successfully.",
                        employee = employee
                    });
                }  
               return BadRequest("Check if the store is exist or not.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the employee.");
            }
        }



        // return data
        [HttpGet]
        [Route("All Employees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employee = appDbContext.Employees.Where(e => e.IsActive).ToList();
                //var employee = appDbContext.Employees.ToList();
                if (employee == null || !employee.Any(e => e.IsActive))
                {
                    return NotFound("No employees found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the employees: {ex.Message}");
            }

        }





        [HttpGet]
        [Route("All Deleted Employees")]
        public IActionResult GetAllDeletedEmployees()
        {
            try
            {
                var employee = appDbContext.Employees.Where(e => e.IsActive == false).ToList();
                //var employee = appDbContext.Employees.ToList();
                if (employee == null || !employee.Any(e => e.IsActive == false))
                {
                    return NotFound("No employees found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the employees: {ex.Message}");
            }

        }



        // return data by id
        [HttpGet("Get Employee Information/{id:int}")]
        public IActionResult GetEmployeeByID([FromRoute] int id)
        {
            try
            {
                var employee = appDbContext.Employees
                    .Include(s => s.Store)
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.EmployeeId == id && p.IsActive);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound($"No employees found with this id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }







        [HttpGet("Get Order Details maked by employee Id")]
        public IActionResult GetEmployeeOrderDetails(int id)
        {
            try
            {
                var employee = appDbContext.Employees
                    .Include(s => s.Store)
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.EmployeeId == id && p.IsActive);
                if (employee != null)
                {
                    var orderdetails = employee.Orders
                        .Where(o => o.IsExist && o.EmployeeId == id)
                        .Select( o => new EmployeeOrderShortDto
                        {
                            OrderId = o.OrderId,
                            OrderDate = o.OrderDate,
                            ShippedDate = o.ShippedDate,
                            TotalAmount = o.TotalAmount,    
                        }).ToList();

                    if(!employee.Orders.Any(o => o.IsExist))
                    {
                        return Ok(new
                        {
                            EmployeeName = employee.EmployeeName,
                            massage = "This employee does not make any orders."
                        });
                    }

                    var employeeDto = new EmployeeMakedOrdersDto
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        employeeOrderShortDtos = orderdetails
                    };
                    
                    return Ok(employeeDto);               
                }                
                return NotFound($"No employees found with this id.");                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        [HttpGet("{name}")]
        public IActionResult GetEmployeeByName([FromRoute] string name)
        {
            try
            {
                var employee = appDbContext.Employees
                    .Include(s => s.Store)
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.EmployeeName.ToLower() == name.ToLower() && p.IsActive);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound($"No employees found with this name.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        [HttpGet("Get Order Details maked by employee/{name}")]
        public IActionResult GetEmployeeOrderDetails(string name)
        {
            try
            {
                var employee = appDbContext.Employees
                    .Include(s => s.Store)
                    .Include(o => o.Orders)
                    .FirstOrDefault(p => p.EmployeeName.ToLower() == name.ToLower() && p.IsActive);
                if (employee != null)
                {
                    var orderdetails = employee.Orders
                        .Where(o => o.IsExist)
                        .Select(o => new EmployeeOrderShortDto
                        {
                            OrderId = o.OrderId,
                            OrderDate = o.OrderDate,
                            ShippedDate = o.ShippedDate,
                            TotalAmount = o.TotalAmount,
                        }).ToList();

                    if (!employee.Orders.Any(o => o.IsExist))
                    {
                        return Ok(new
                        {
                            EmployeeName = employee.EmployeeName,
                            massage = "This employee does not make any orders."
                        });
                    }

                    var employeeDto = new EmployeeMakedOrdersDto
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        employeeOrderShortDtos = orderdetails
                    };

                    return Ok(employeeDto);
                }
                return NotFound($"No employees found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                var CheckStoreExsist = appDbContext.Stores.FirstOrDefault(s => s.StoreId == employee.StoreId && s.IsExist);
                
                if (CheckStoreExsist == null)
                    return BadRequest("Check if the store is exist or not.");

                var old_employee = appDbContext.Employees.FirstOrDefault(p => p.EmployeeId == id);
                if (old_employee != null)
                {
                    old_employee.EmployeeName = employee.EmployeeName;
                    old_employee.EmployeeEmail = employee.EmployeeEmail;
                    old_employee.EmployeePhone = employee.EmployeePhone;
                    old_employee.EmployeeSalary = employee.EmployeeSalary;
                    old_employee.StoreId = employee.StoreId;
                    old_employee.IsActive = employee.IsActive;
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Employee updated Successfully.",
                        employee = old_employee
                    });
                }
                return NotFound($"No employees found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var employee = appDbContext.Employees.FirstOrDefault(p => p.EmployeeId == id && p.IsActive);
                if (employee != null)
                {
                    employee.IsActive = false;
                    //appDbContext.Employees.Remove(employee);
                    appDbContext.SaveChanges();
                    return Ok("Employee deleted successfully.");
                }
                return NotFound($"No employees found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
