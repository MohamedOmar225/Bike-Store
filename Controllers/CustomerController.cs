using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using bike_store_2.Repositories;
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

       
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository _customerRepository)
        {
            this._customerRepository = _customerRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromForm] CreateCustomerDTO createCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerRepository.CreateCustomerAsync(createCustomer);
                return Ok(new
                {
                    Message = "Customer added successfully.",
                    Customer = customer
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the customer: {ex.Message}");
            }

        }



        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromForm] UpdateCustomerDto updateCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerRepository.UpdateCustomerAsync(id, updateCustomer);
                return Ok(new
                {
                    Message = "Customer updated successfully.",
                    Customer = customer
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the customer: {ex.Message}");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All Active Customers")]
        public async Task<IActionResult> GetAllExistingCustomers()
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customers = await _customerRepository.GetAllExistingCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the customers: {ex.Message}");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All Deleted Customers")]
        public async Task<IActionResult> GetAllDeletedCustomers()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customers = await _customerRepository.GetAllDeletedCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the customers: {ex.Message}");
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Get Customer Information/{id:int}")]
        public async Task<IActionResult> GetCustomerByID([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if(customer == null)
                    return BadRequest($"No customers found with this id {id}.");

                return Ok(customer);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Get Customer Information/{name}")]
        public async Task<IActionResult> GetCustomerByName([FromRoute] string name)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerRepository.GetCustomerByNameAsync(name);
                if (customer == null)
                    return BadRequest($"No customers found with this name {name}.");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _customerRepository.DeleteCustomerAsync(id);
                return Ok("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }







    }
}
