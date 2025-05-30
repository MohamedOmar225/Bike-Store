using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using bike_store_2.Repositories;
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

        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm]CreateEmployeeDTO employeeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var employee = await _employeeRepository.CreateEmployeeAsync(employeeDTO);
                return Ok(new
                {
                    Message = "Employee added successfully.",
                    employee = employee
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the employee: {ex.Message}");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromForm]UpdateEmployeeDTO updateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                var employee = await _employeeRepository.UpdateEmployeeAsync(employeeId, updateDTO);
                return Ok(new
                {
                    Message = "Employee updated successfully.",
                    employee = employee
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the employee: {ex.Message}");
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All Active Employees")]
        public async Task<IActionResult> GetAllExistingEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllActiveEmployeesAsync();
                if (employees == null || !employees.Any())                
                    return NotFound("No employees found.");
                
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the employees: {ex.Message}");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All Deleted Employees")]
        public async Task<IActionResult> GetAllDeletedEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllDeletingEmployeesAsync();
                if (employees == null || !employees.Any())
                    return NotFound("No employees found.");

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the employees: {ex.Message}");
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeByID(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
                if (employee != null)                
                    return Ok(employee);
                
                return NotFound($"No employees found with this id.");               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the employee: {ex.Message}");
            }
        }





        [Authorize(Roles = "Admin")]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetEmployeeByName(string employeeName)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByNameAsync(employeeName);
                if (employee != null)
                    return Ok(employee);

                return NotFound($"No employees found with this name.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the employee: {ex.Message}");
            }
        }






        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(employeeId);
                return Ok("Employee deleted successfully.");
            }         
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the employee: {ex.Message}");
            }
        }


    }
}
