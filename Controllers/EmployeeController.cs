using bike_store_2.Data;
using bike_store_2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult CreateProduct([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                appDbContext.Employees.Add(employee);
                appDbContext.SaveChanges();
                return Ok(employee);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }



        // return data
        [HttpGet]
        public IActionResult GetActionResult()
        {
            try
            {
                var employee = appDbContext.Employees.ToList();
                if (employee == null)
                {
                    return NotFound("No employees found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the products: {ex.Message}");
            }

        }



        // return data by id
        [HttpGet("{id:int}")]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var employee = appDbContext.Employees
                    .Include(s => s.Store)
                    .FirstOrDefault(p => p.Emp_id == id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataProduct([FromRoute] int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_employee = appDbContext.Employees
                    .Include(s => s.Store)
                    .FirstOrDefault(p => p.Emp_id == id);
                if (old_employee != null)
                {
                    old_employee.Emp_name = employee.Emp_name;
                    old_employee.Emp_phone = employee.Emp_phone;
                    old_employee.Emp_Email = employee.Emp_Email;
                    old_employee.Emp_salary = employee.Emp_salary;
                    old_employee.Store_id = employee.Store_id;
                    appDbContext.SaveChanges();
                    return Ok(old_employee);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
     


        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteByID(int id)
        {
            try
            {
                var employee = appDbContext.Employees.FirstOrDefault(p => p.Emp_id == id);
                if (employee != null)
                {
                    appDbContext.Employees.Remove(employee);
                    appDbContext.SaveChanges();
                    return Ok("Employee deleted successfully.");
                }
                return NotFound("Employee not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }









    }
}
