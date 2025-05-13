using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
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
        private readonly AppDbContext appDbContext;
        public BrandController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new product
        [HttpPost]
        public IActionResult CreateBrand([FromBody] Brand brand)
        {           
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                appDbContext.Brands.Add(brand);
                appDbContext.SaveChanges();
                return Ok(new
                {
                    Massage = "Brand added Successfully.",
                    Brand = brand   
                });
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the brand.");
            }
        }



        // return data
        [HttpGet]
        [Route("All Brands")]
        public IActionResult GetAllBrands()
        {
            try
            {
                var brands = appDbContext.Brands.Where(b => b.IsExist).ToList();
                if (brands == null)
                {
                    return NotFound("No brands found.");
                }
                return Ok(brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the brands: {ex.Message}");
            }

        }


        // return data by id
        [HttpGet("{id:int}")]
        public IActionResult GetBrandByID([FromRoute] int id)
        {
            try
            {
                var brand = appDbContext.Brands
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.BrandId == id && p.IsExist);
                if (brand != null)
                {
                    return Ok(brand);
                }
                else
                {
                    return NotFound($"No brands found with this id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("Get Products under spcific brand brandId")]
        public IActionResult GetBrandWithCategories(int brandId)
        {
            try
            {
                var brand = appDbContext.Brands
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.BrandId == brandId && c.IsExist);
                if (brand != null)
                {
                    var ActiveProducts = brand.Products
                        .Where(p => p.IsExisit)
                        .Select(c => new ProductDTO
                        {
                            ProductName = c.ProductName,
                        }).ToList();

                    if (!brand.Products.Any(p => p.IsExisit))
                    {
                        return Ok(new
                        {
                            BrandName = brand.BrandName,
                            massage = "No products found under this brand."
                        });
                    }

                    var result = new BrandWithCategoriesDto
                    {
                        BrandName = brand.BrandName,
                        ProductDTO = ActiveProducts
                    };
                    return Ok(result);
                }

                return NotFound("No brands found with this ID");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        // return data by name
        [HttpGet("{name}")]
        public IActionResult GetBrandByName([FromRoute] string name)
        {
            try
            {
                var brand = appDbContext.Brands
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.BrandName.ToLower() == name.ToLower() && p.IsExist);
                if (brand != null)
                {
                    return Ok(brand);
                }
                else
                {
                    return NotFound($"No brands found with this name.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpGet("Get Products under spcific brand name")]
        public IActionResult GetBrandWithCategories(string brandname)
        {
            try
            {
                var brand = appDbContext.Brands
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.BrandName.ToLower() == brandname.ToLower() && c.IsExist);
                if (brand != null)
                {
                    var ActiveProducts = brand.Products
                        .Where(p => p.IsExisit)
                        .Select(c => new ProductDTO
                        {
                            ProductName = c.ProductName,
                        }).ToList();

                    if (!brand.Products.Any(p => p.IsExisit))
                    {
                        return Ok(new
                        {
                            BrandName = brand.BrandName,
                            massage = "No products found under this brand."
                        });
                    }

                    var result = new BrandWithCategoriesDto
                    {
                        BrandName = brand.BrandName,
                        ProductDTO = ActiveProducts
                    };
                    return Ok(result);
                }

                return NotFound("No brands found with this ID");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataBrands([FromRoute] int id, [FromBody] Brand brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_brand = appDbContext.Brands
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.BrandId == id && p.IsExist);
                if (old_brand != null)
                {
                    old_brand.BrandName = brand.BrandName;
                    old_brand.IsExist = brand.IsExist;
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Brand updated Successfully.",
                        brand = old_brand,
                    });
                }
                return NotFound($"No brands found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBrandByID(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var brand = appDbContext.Brands.FirstOrDefault(p => p.BrandId == id && p.IsExist);
                var proudcts = appDbContext.Products.Where(p => p.BrandId == id && p.IsExisit).ToList();                
                if (brand != null && proudcts != null)
                {                   
                    foreach (var product in proudcts)
                    {                                            
                        product.IsExisit = false;                           
                    }                    
                    brand.IsExist = false;
                    appDbContext.SaveChanges();
                    return Ok("Brand deleted successfully.");
                }
                return NotFound($"No brands found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

