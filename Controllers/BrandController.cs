using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using bike_store_2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        

        private readonly IBrandRepository _brandRepository;
        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandDto createBrand)
        {
            try
            {
                if (!ModelState.IsValid)                
                    return BadRequest(ModelState);
                
                var brand = await _brandRepository.CreateBrandAsync(createBrand);
                return Ok(new
                {
                    Message = "Brand added Successfully.",
                    Brand = brand
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the brand: {ex.Message}");
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromForm] UpdateBrandDto updateBrand)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var brand = await _brandRepository.UpdateBrandAsync(id, updateBrand);
                return Ok(new
                {
                    Message = "Brand updated Successfully.",
                    Brand = brand
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the brand: {ex.Message}");
            }
        }




        [Authorize(Roles = "User")]
        [HttpGet("All Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var brands = await _brandRepository.GetAllExistingBrandsAsync();
                if (brands == null || !brands.Any())
                    return NotFound("No brands found.");

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the brands: {ex.Message}");
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet("All Deleted Brands")]
        public async Task<IActionResult> GetAllDeletedBrands()
        {
            try
            {
                var brands = await _brandRepository.GetAllDeletedBrandsAsync();
                if (brands == null || !brands.Any())
                    return NotFound("No brands found.");

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the brands: {ex.Message}");
            }
        }




        [Authorize(Roles = "User")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrandByID(int id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandByIdAsync(id);
                if (brand != null)
                    return Ok(brand);

                return NotFound($"No brands found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [Authorize(Roles = "User")]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetBrandByName(string name)
        {
            try
            {
                var brand = await _brandRepository.GetBrandByNameAsync(name);
                if (brand != null)
                    return Ok(brand);

                return NotFound($"No brands found with this name.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBrandByID(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                await _brandRepository.DeleteBrandAsync(id);
                return Ok("Brand deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


    }
}

