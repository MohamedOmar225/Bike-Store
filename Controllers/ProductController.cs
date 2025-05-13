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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // create new product
        [HttpPost]
        public IActionResult CreateProductWithStores([FromBody] AddProductToStore productdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = appDbContext.Categories.FirstOrDefault(c => c.CategoryId == productdto.CategoryId && c.IsExsit);
                var brand = appDbContext.Brands.FirstOrDefault(b => b.BrandId == productdto.BrandId && b.IsExist);

                if (category != null && brand != null)
                {
                    var Product = new Product
                    {
                        ProductName = productdto.ProductName,
                        ListPrice = productdto.ListPrice,
                        ModelYear = productdto.ModelYear,
                        CategoryId = productdto.CategoryId,
                        BrandId = productdto.BrandId,
                        IsExisit = true
                    };

                    appDbContext.Products.Add(Product);
                    appDbContext.SaveChanges();

                    // اضافه المنتج في المحل
                    foreach (var product in productdto.stoerQuantities)
                    {
                        var stores = appDbContext.Stores.FirstOrDefault(s => s.StoreId == product.StoreId && s.IsExist);
                        if (stores != null)
                        {
                            var productStore = new ProductStore
                            {
                                ProductId = Product.ProductId,
                                StoreId = product.StoreId,
                                Quanttity = product.Quantity
                            };
                            appDbContext.ProductStores.Add(productStore);
                        }
                    }
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Product created and assigned to stores successfully.",
                        Product = productdto
                    });
                }
                return BadRequest("Invalid Category or Brand");
            }                    
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // return data
        [HttpGet]
        [Route("All Products")]
        public IActionResult GetAllProducts()
        {
            try
            {
                //var products = appDbContext.Products.ToList();
                var products = appDbContext.Products.Where(p => p.IsExisit).ToList();
                if (products == null || !products.Any())
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
                    .Include(c => c.Category).Include(s => s.Stores).Include(b => b.Brands).Include(oi => oi.OrderItems)
                    .FirstOrDefault(p => p.ProductId == id && p.IsExisit);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound($"No products found with this id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // get by name
        [HttpGet("{name}")]
        public IActionResult GetProductByName([FromRoute] string name)
        {            
            try
            {
                var product = appDbContext.Products
                    .Include(c => c.Category).Include(s => s.Stores).Include(b => b.Brands).Include(oi => oi.OrderItems)
                    .FirstOrDefault(p => p.ProductName == name && p.IsExisit);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound($"No products found with this name.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


       

        // update data
        [HttpPut("{id:int}")]
        public IActionResult UpdataProduct([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                var category = appDbContext.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId && c.IsExsit);
                var brand = appDbContext.Brands.FirstOrDefault(b => b.BrandId == product.BrandId && b.IsExist);

                if (brand == null && category == null)
                    return BadRequest("Invalid Category or Brand");

                var old_product = appDbContext.Products
                    .Include(c => c.Category)
                    .FirstOrDefault(p => p.ProductId == id);
                if (old_product != null)
                {
                    old_product.ProductName = product.ProductName;
                    old_product.ModelYear = product.ModelYear;
                    old_product.ListPrice = product.ListPrice;
                    old_product.BrandId = product.BrandId;
                    old_product.CategoryId = product.CategoryId;
                    old_product.IsExisit = product.IsExisit;
                    appDbContext.SaveChanges();
                    return Ok(new
                    {
                        Massage = "Product updated successfully.",
                        Product = old_product
                    });
                }
                return NotFound($"No products found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProductByID(int id)
        {
            try
            {
                var product = appDbContext.Products.FirstOrDefault(p => p.ProductId == id && p.IsExisit);
                if (product != null)
                {                        
                    product.IsExisit = false;                    
                    var deletefromstore = appDbContext.ProductStores.Where(p => p.ProductId == id).ToList();
                    if (deletefromstore != null)
                    {
                        appDbContext.RemoveRange(deletefromstore);
                    }

                    appDbContext.SaveChanges();
                    return Ok("Product deleted from all stores successfully.");
                }
                return NotFound($"No products found with this id.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }











        //**********************************************************************

        //[HttpPost]
        //public IActionResult CreateProduct([FromBody] Product product , int StoreId , int quantity)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var checkCategoryId = appDbContext.Categories.Where(x => x.CategoryId == product.CategoryId && x.IsExsit);
        //        var checkBrandId = appDbContext.Brands.Where(x => x.BrandId == product.BrandId && x.IsExist);

        //        if (checkCategoryId != null && checkBrandId != null)
        //        {

        //            var checkStore = appDbContext.Stores.Where(s => s.StoreId == StoreId);
        //            if (checkStore != null)
        //            {
        //                appDbContext.Products.Add(product);
        //                appDbContext.SaveChanges();

        //                var newproduct = new ProductStore
        //                {
        //                    StoreId = StoreId,
        //                    ProductId = product.ProductId,
        //                    Quanttity = quantity,
        //                };
        //                appDbContext.ProductStores.Add(newproduct);
        //                appDbContext.SaveChanges();

        //                return Ok(new
        //                {
        //                    massage = "Product created and added into store successfully.",
        //                    product = product,
        //                });
        //            }
        //            return NotFound("Store does not exist.");

        //        }
        //        return BadRequest("Check if the category or brand are existing or not.");
        //    }
        //    catch
        //    {
        //        return StatusCode(500, "An error occurred while creating the product.");
        //    }
        //}













       




    }
}
