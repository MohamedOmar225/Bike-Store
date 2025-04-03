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
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public BrandController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new product
        [HttpPost]
        public IActionResult CreateBrand([FromBody] Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                appDbContext.Brands.Add(brand);
                appDbContext.SaveChanges();
                return Ok(brand);
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }



        // return data
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var brands = appDbContext.Brands.ToList();
                if (brands == null)
                {
                    return NotFound("No brands found.");
                }
                return Ok(brands);
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
                var brand = appDbContext.Brands
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.brand_id == id);
                if (brand != null)
                {
                    return Ok(brand);
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
        public IActionResult UpdataProduct([FromRoute] int id, [FromBody] Brand brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_brand = appDbContext.Brands
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.brand_id == id);
                if (old_brand != null)
                {
                    old_brand.brand_name = brand.brand_name;                   
                    appDbContext.SaveChanges();
                    return Ok(old_brand);
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
                var brand = appDbContext.Brands.FirstOrDefault(p => p.brand_id == id);
                var deleteProductForthisBrand = appDbContext.Brands.Find();
                if (brand != null)
                {
                    appDbContext.Brands.Remove(brand);
                    appDbContext.SaveChanges();
                    return Ok("Brand deleted successfully.");
                }
                return NotFound("Brand not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
