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
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public StoreController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new product
        [HttpPost]
        public IActionResult CreateStore([FromBody] Store store)
        {           
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                appDbContext.Stores.Add(store);
                appDbContext.SaveChanges();
                return Ok(new
                {
                    Massage = "Store added Successfully.",
                    store = store
                });
            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the store.");
            }
        }



        // return data
        [HttpGet]
        [Route("All Stores")]
        public IActionResult GetAllStores()
        {
            try
            {
                var stores = appDbContext.Stores
                    .Include(s => s.Products).Include(s => s.Orders).Include(s => s.Employees).Where(s => s.IsExist)
                    .ToList();
                if (stores == null)
                {
                    return NotFound("No stores found.");
                }
                return Ok(stores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the stores: {ex.Message}");
            }

        }


        // return data by id
        [HttpGet("{id:int}")]
        public IActionResult GetStoreByID([FromRoute] int id)
        {
            try
            {
                var store = appDbContext.Stores
                    .Include(s => s.Products).Include(s => s.Orders).Include(s => s.Employees)
                    .FirstOrDefault(p => p.StoreId == id && p.IsExist);
                if (store != null)
                {
                    return Ok(store);
                }
                else
                {
                    return NotFound($"No stores found with this id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("Quantity Of Products In Store By Id")]
        public IActionResult QuantityOfProductsInStore(int id)
        {
            try
            {
                var store = appDbContext.Stores
                    .Include(s => s.Products).Include(s => s.Orders).Include(s => s.Employees)
                    .FirstOrDefault(p => p.StoreId == id && p.IsExist);
                if (store != null)
                {
                    var productdetails = appDbContext.ProductStores.Where(ps => ps.StoreId == id)
                        .Include(ps => ps.Products)
                        .Select(ps => new ProductDetails
                        {
                            ProductId = ps.ProductId,
                            ProductName = ps.Products.ProductName,
                            Quantity = ps.Quanttity
                        }).ToList();
                    var storeDto = new Store_and_QuantityofProduct_In_It
                    {
                        StoreName = store.StoreName,
                        ProductDetails = productdetails,
                    };
                    if(!storeDto.ProductDetails.Any())
                        return NotFound($"No products found in this store.");
                    
                    return Ok(storeDto);
                }
                else
                {
                    return NotFound($"No stores found with this id");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("{name}")]
        public IActionResult GetStoreByName([FromRoute] string name)
        {
            try
            {
                var store = appDbContext.Stores
                     .Include(s => s.Products).Include(s => s.Orders).Include(s => s.Employees)
                    .FirstOrDefault(p => p.StoreName == name && p.IsExist);
                if (store != null)
                {
                    return Ok(store);
                }
                else
                {
                    return NotFound($"No customers found with this name.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       



        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataStore([FromRoute] int id, [FromBody] Store store)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }
                var old_store = appDbContext.Stores
                     .Include(s => s.Products).Include(s => s.Orders).Include(s => s.Employees)
                    .FirstOrDefault(p => p.StoreId == id && p.IsExist);
                if (old_store != null)
                {
                    old_store.StoreName = store.StoreName;
                    old_store.city = store.city;
                    old_store.street = store.street;
                    old_store.Phone = store.Phone;
                    old_store.Email = store.Email;
                    old_store.IsExist = store.IsExist;
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Store updated Successfully.",
                        store = old_store
                    });
                }
                return NotFound($"No stores found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       



        [HttpDelete]
        public IActionResult DeleteStoreByID(int DeleteStoreId, int alternativeStoreId)
        {
            try
            {
                var store = appDbContext.Stores.FirstOrDefault(p => p.StoreId == DeleteStoreId && p.IsExist);
                var employees = appDbContext.Employees.Where(e => e.StoreId == DeleteStoreId && e.IsActive).ToList();
                var productlist = appDbContext.ProductStores.Where(e => e.StoreId == DeleteStoreId).ToList();

                if (store != null && employees != null && productlist != null)
                {
                    foreach (var employee in employees)
                    {
                        employee.IsActive = false;
                    }
                    foreach (var PS in productlist)
                    {
                        var product = appDbContext.ProductStores.FirstOrDefault
                                        (ps => ps.StoreId == alternativeStoreId && ps.ProductId == PS.ProductId);
                        if (product != null)
                        {
                            product.Quanttity += PS.Quanttity;
                        }
                        else
                        {
                            var newProStore = new ProductStore
                            {
                                StoreId = alternativeStoreId,
                                ProductId = PS.ProductId,
                                Quanttity = PS.Quanttity,
                            };
                            appDbContext.ProductStores.Add(newProStore);
                        }

                        appDbContext.Remove(PS);
                    }
                    store.IsExist = false;
                    appDbContext.SaveChanges();
                    return Ok("Store deleted successfully.");
                }
                return NotFound($"No srores found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
