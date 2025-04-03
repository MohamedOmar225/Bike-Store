using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public ProductController(AppDbContext  appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new product
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            try
            {
                var checkCategoryId = appDbContext.Categories.Find(product.cate_id);
                var checkBrandId = appDbContext.Brands.Find(product.brand_id);
                if(checkCategoryId != null && checkBrandId != null)
                {
                    appDbContext.Products.Add(product);
                    appDbContext.SaveChanges();
                    return Ok(product);
                }
                return BadRequest("Check category id or brand id.");               
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
                var products = appDbContext.Products.ToList();                    
                if (products == null)
                {
                    return NotFound("No products found.");
                }                
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the products: {ex.Message}");
            }

        }


        // return data by id
        [HttpGet("{id:int}")]
        public IActionResult GetProductByID([FromRoute] int id)
        {
            try
            {
                var product = appDbContext.Products
                    .Include(c => c.Category).Include(b => b.Brands)
                    .FirstOrDefault(p => p.product_id == id);                
                if (product != null)
                {                                                        
                    return Ok(product);
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



        //public IActionResult GetProductNameListpriceMoelyearandCategoryNameByproductId([FromRoute] int id)
        //{
        //    try
        //    {
        //        var product = appDbContext.Products
        //            .Include(c => c.Category).Include(b => b.Brands)
        //            .FirstOrDefault(p => p.product_id == id);
        //        GetProductsandCategoryNameDTO getProduct = new GetProductsandCategoryNameDTO();
        //        if (product != null)
        //        {
        //            getProduct.Name = product.product_name;
        //            getProduct.Price = product.list_price;
        //            getProduct.model_year = product.model_year;
        //            getProduct.category_name = product.Category.cate_name;
        //            return Ok(getProduct);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }

        //}


        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataProduct([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_product = appDbContext.Products
                    .Include(c => c.Category).Include(b => b.Brands)
                    .FirstOrDefault(p => p.product_id == id);
                if (old_product != null)
                {
                    old_product.product_name = product.product_name;
                    old_product.model_year = product.model_year;
                    old_product.list_price = product.list_price;
                    old_product.brand_id = product.brand_id;
                    old_product.cate_id = product.cate_id;
                    appDbContext.SaveChanges();
                    return Ok(old_product);
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
                var product = appDbContext.Products.FirstOrDefault(p => p.product_id == id);
                if (product != null)
                {
                    appDbContext.Products.Remove(product);
                    appDbContext.SaveChanges();
                    return Ok("Product deleted successfully.");
                }
                return NotFound("Product not found.");               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
