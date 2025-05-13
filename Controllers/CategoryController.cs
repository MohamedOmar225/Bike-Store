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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                appDbContext.Categories.Add(category);
                appDbContext.SaveChanges();
                return Ok(new
                {
                    Massage = "Category added Successfully.",
                    Category = category
                });
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }


        // return data 
        [HttpGet]
        [Route("All category")]
        public IActionResult GetAllcategory()
        {
            try
            {
                var catrgories = appDbContext.Categories.Where(c => c.IsExsit).ToList();
                if (catrgories != null)
                {
                    return Ok(catrgories);

                }
                return NotFound("No categories found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the categories: {ex.Message}");
            }

        }


        [HttpGet("cate-ID&NameDTO")]
        public IActionResult GetCategoryIdandNameDTO()
        {
            try
            {
                var catrgories = appDbContext.Categories.Where(c => c.IsExsit).ToList();
                if (catrgories != null)
                {
                    List<GetCategoryIdandNameDTO> getCategoryIdandNameDTO = new List<GetCategoryIdandNameDTO>();

                    foreach (var item in catrgories)
                    {
                        getCategoryIdandNameDTO.Add(new GetCategoryIdandNameDTO()
                        {
                            Id = item.CategoryId,
                            Name = item.CategoryName
                        });
                    }
                    return Ok(getCategoryIdandNameDTO);
                }
                return NotFound($"No categories found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the categories: {ex.Message}");
            }

        }


        // return data by id
        [HttpGet("{id:int}")]
        public IActionResult GetCategoryByID([FromRoute] int id)
        {
            try
            {
                var category = appDbContext.Categories
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.CategoryId == id && p.IsExsit);
                if (category != null)
                {
                    return Ok(category);
                }
                else
                {
                    return NotFound($"No categories found with this id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // get by category name
        [HttpGet("{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            try
            {
                var category = appDbContext.Categories
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.CategoryName.ToLower() == name.ToLower() && p.IsExsit);
                if (category != null)
                {
                    return Ok(category);
                }
                else
                {
                    return NotFound($"No categories found with this name.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("Products details under a specific categoryDTO")]
        public IActionResult ListAllProductsNameForSpecificCategoryDTO(int CategoryId)
        {
            try
            {
                Category? category = appDbContext.Categories
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.CategoryId == CategoryId && p.IsExsit);
                if (category != null)
                {

                    var productdetails = category.Products
                        .Where(p => p.IsExisit)
                        .Select(p => new ProductDetailsDTO
                        {
                            ProductName = p.ProductName,
                            price = p.ListPrice,
                            ModelYear = p.ModelYear,
                        }).ToList();

                    if (!category.Products.Any(p => p.IsExisit))
                    {
                        return Ok(new
                        {
                            ProductName = category.Products.First().ProductName,
                            massage = "This product does not exist under this category."
                        });
                    }

                    var productdto = new List_all_products__for__specific_categoryDTO
                    {
                        CategoryName = category.CategoryName,
                        ProductDetails = productdetails
                    };

                    return Ok(productdto);
                }
                return NotFound($"No categories found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // update category 
        [HttpPut("{id:int}")]
        public IActionResult UpdataCategory([FromRoute] int id, [FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_category = appDbContext.Categories
                    .Include(p => p.Products)
                    .FirstOrDefault(p => p.CategoryId == id);
                if (old_category != null)
                {
                    old_category.CategoryName = category.CategoryName;
                    old_category.IsExsit = category.IsExsit;
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Category updated Successfully.",
                        category = old_category,                           
                    });
                }
                return NotFound($"No categories found with this id.");
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
                var category = appDbContext.Categories.FirstOrDefault(p => p.CategoryId == id && p.IsExsit);
                var products = appDbContext.Products.Where(p => p.CategoryId == id && p.IsExisit).ToList();
                if (category != null && products != null)
                {
                    foreach (var product in products)
                    {                        
                        product.IsExisit = false;
                    }                    
                    category.IsExsit = false;
                    appDbContext.SaveChanges();
                    return Ok("category deleted successfully.");
                }
                return NotFound($"No categories found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }









        //*************************************************







       










    }
}
