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
    public class CategoryController : ControllerBase
    {


        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext appDbContext;



        public CategoryController(ICategoryRepository _categoryRepository , AppDbContext appDbContext)
        {
            this._categoryRepository = _categoryRepository;
            this.appDbContext = appDbContext;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryDTO categoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdCategory = await _categoryRepository.CreateCategoryAsync(categoryDTO);
                return Ok(new
                {
                    Message = "Category added successfully.",
                    Category = createdCategory
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the category: {ex.Message}");
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] CategoryUpdateDTO updateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = await _categoryRepository.GetCategoryByIdAsync(id);

                if (category == null)
                    return NotFound($"No categories found with this id.");


                var updatedCategory = await _categoryRepository.UpdateCategoryAsync(id, updateDTO);
                return Ok(new
                {
                    Message = "Category updated successfully.",
                    Category = updatedCategory
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
            }

        }






        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("All Existing categories")]
        public async Task<IActionResult> GetAllExistingCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllExistingCategoriesAsync();
                if (categories != null)
                    return Ok(categories);

                return NotFound("No categories found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the categories: {ex.Message}");
            }
        }





        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All Deleted categories")]
        public async Task<IActionResult> GetAllDeletedCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllDeletedCategoriesAsync();
                if (categories != null)
                    return Ok(categories);

                return NotFound("No categories found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the categories: {ex.Message}");
            }
        }






        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);

                if (category != null)
                    return Ok(category);

                return NotFound($"No categories found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the category: {ex.Message}");
            }
        }



        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByNameAsync(name);

                if (category == null)
                    return NotFound($"No categories found with this name.");

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the category: {ex.Message}");
            }
        }






        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound($"No categories found with this id.");

                await _categoryRepository.DeleteCategoryAsync(id);
                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the category: {ex.Message}");
            }

        }
       
    }
}
