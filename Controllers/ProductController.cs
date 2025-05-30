using bike_store_2.DTO;
using bike_store_2.Repositories;
using bike_store_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static bike_store_2.DTO.ProductDetailsdto;


namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _product_repo;
        public ProductController(IProductRepository product_repo)
        {
            _product_repo = product_repo;

        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromForm] AddProductToStore productdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _product_repo.CreateProductAsync(productdto);
                await _product_repo.SaveProductImageAsync(product.ProductId, product.ProductName, productdto.ImageFiles);

                return Ok(new
                {
                    Massage = "Product created successfully.",
                    Product = productdto,
                    imageCount = productdto.ImageFiles.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                var old_product = await _product_repo.UpdateProductAsync(id, updateProductDto);
                await _product_repo.UpdateProductImageAsync(id, updateProductDto.MainImage);

                return Ok(new
                {
                    Massage = "Product updated successfully.",
                    Product = old_product
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [Authorize(Roles = "User")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByID([FromRoute] int id)
        {
            try
            {
                var Product = await _product_repo.GetProductByIdAsync(id);

                if (Product == null)
                    return NotFound($"No products found with this id {id}.");

                return Ok(Product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "User")]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            try
            {
                var product = await _product_repo.GetProductByNameAsync(name);
                if (product == null)
                    return NotFound($"No products found with this name {name}.");

                return Ok(product);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("All Existing Products")]
        public async Task<IActionResult> GetAllExistingProductsAsync()
        {
            try
            {
                var products = await _product_repo.GetAllExistProductsAsync();
                if (products == null || !products.Any())
                    return NotFound("No products found.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the products: {ex.Message}");
            }
        }





        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All Deleted Products")]
        public async Task<IActionResult> GetAllDeletedProductsAsync()
        {
            try
            {
                var products = await _product_repo.GetAllDeletedProductsAsync();
                if (products == null || !products.Any())
                    return NotFound("No products found.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the products: {ex.Message}");
            }
        }









        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProductByID(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                await _product_repo.DeleteProductAsync(id);

                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Uploade Image")]
        public async Task<IActionResult> UploadeImages([FromForm] UploadeImagesForProducts uploadeImages)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data provided.");

                await _product_repo.UploadImages(uploadeImages.productId, uploadeImages.ProductName, uploadeImages.ImageFile);

                return Ok("Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }

        }






        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Delete Image")]
        public async Task<IActionResult> DeleteImages(int ProductId, int ImageId)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided.");

            await _product_repo.DeleteImagesAsync(ProductId, ImageId);
            return Ok("Image deleted successfully");

        }



        



    }
}