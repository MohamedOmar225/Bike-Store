using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using bikestore2.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {

        private readonly AppDbContext appDbContext;
        public CategoryController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // create new category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            try
            {
                appDbContext.Categories.Add(category);
                appDbContext.SaveChanges();
                return Ok(category);
            }
            catch
            {                
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }


        // return data 
        [HttpGet]
        public IActionResult GetAllcategory()
        {
            try
            {
                var catrgories = appDbContext.Categories.ToList();
                if (catrgories != null)
                {                                                           
                    return Ok(catrgories);
                    
                }
                return NotFound("No categories found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the products: {ex.Message}");
            }

        }


        //[HttpGet]
        //public IActionResult GetCategoryIdandNameDTO()
        //{
        //    try
        //    {
        //        var catrgories = appDbContext.Categories.ToList();
        //        if (catrgories != null)
        //        {
        //            List<GetCategoryIdandNameDTO> getCategoryIdandNameDTO = new List<GetCategoryIdandNameDTO>();

        //            foreach (var item in catrgories)
        //            {
        //                getCategoryIdandNameDTO.Add(new GetCategoryIdandNameDTO()
        //                {
        //                    Id = item.cate_id,
        //                    Name = item.cate_name
        //                });
        //            }
        //            return Ok(getCategoryIdandNameDTO);

        //        }
        //        return NotFound("No categories found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while fetching the products: {ex.Message}");
        //    }

        //}


        // return data by id
        [HttpGet("{id:int}")]
        public IActionResult GetCategoryByID([FromRoute] int id)
        {
            try
            {
                var category = appDbContext.Categories
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.cate_id == id);                
                if (category != null)
                {                                       
                    return Ok(category);
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



        //[HttpGet("{id:int}")]
        //public IActionResult ListAllProductsNameForSpecificCategoryDTO([FromRoute] int id)
        //{
        //    try
        //    {
        //        var category = appDbContext.Categories
        //            .Include(p => p.Products)
        //            .FirstOrDefault(p => p.cate_id == id);
        //        List_all_products__for__specific_categoryDTO allproduct = new List_all_products__for__specific_categoryDTO();
        //        if (category != null)
        //        {
        //            allproduct.Category_Name = category.cate_name;
        //            foreach (var product in category.Products)
        //            {
        //                allproduct.Product_Name.Add(product.product_name);
        //            }
        //            return Ok(allproduct);
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


        // update category 
        [HttpPut("{id:int}")]
        public IActionResult UpdataProduct([FromRoute] int id, [FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_category = appDbContext.Categories
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.cate_id == id);
                if (old_category != null)
                {
                    old_category.cate_name = category.cate_name;
                    appDbContext.SaveChanges();
                    return Ok(old_category);
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
                var category = appDbContext.Categories.FirstOrDefault(p => p.cate_id == id);
                if (category != null)
                {
                    appDbContext.Categories.Remove(category);
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
