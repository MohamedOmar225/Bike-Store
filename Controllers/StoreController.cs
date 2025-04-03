using bike_store_2.Data;
using bike_store_2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public StoreController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new product
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                appDbContext.Stores.Add(store);
                appDbContext.SaveChanges();
                return Ok(store);
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
                var stores = appDbContext.Stores.ToList();
                if (stores == null)
                {
                    return NotFound("No stores found.");
                }
                return Ok(stores);
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
                var store = appDbContext.Stores
                    .Include(e => e.Employees)
                    .FirstOrDefault(p => p.store_id == id);
                if (store != null)
                {
                    return Ok(store);
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
        public IActionResult UpdataProduct([FromRoute] int id, [FromBody] Store store)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_store = appDbContext.Stores
                    .Include(e => e.Employees)
                    .FirstOrDefault(p => p.store_id == id);
                if (old_store != null)
                {
                    old_store.store_name = store.store_name;
                    old_store.street = store.street;                    
                    appDbContext.SaveChanges();
                    return Ok(old_store);
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
                var store = appDbContext.Stores.FirstOrDefault(p => p.store_id == id);
                if (store != null)
                {
                    appDbContext.Stores.Remove(store);
                    appDbContext.SaveChanges();
                    return Ok("Store deleted successfully.");
                }
                return NotFound("Store not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
