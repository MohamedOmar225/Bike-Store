using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using bike_store_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static bike_store_2.DTO.ProductDetailsdto;
using static System.Net.Mime.MediaTypeNames;


namespace bike_store_2.Repositories
{

    public interface IProductRepository
    {
        Task<IEnumerable<ProductDetailsdto>> GetAllExistProductsAsync();
        Task<IEnumerable<ProductDetailsdto>> GetAllDeletedProductsAsync();
        Task<ProductDetailsdto> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDetailsdto>> GetProductByNameAsync(string productname);
        Task<Product> CreateProductAsync(AddProductToStore productdto);
        Task SaveProductImageAsync(int id, string name , List<IFormFile> ImageFile);        
        Task UpdateProductImageAsync(int id, int ImageId);        
        Task<Product> UpdateProductAsync( int id , UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
        Task UploadImages(int productId, string name, List<IFormFile> images);        
        Task DeleteImagesAsync(int productId , int ImageId);                
    }



    public class ProductRepository : IProductRepository
    {
        private readonly IProductFileService _productFileService;
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext, IProductFileService productFileService)
        {
            _appDbContext = appDbContext;
            _productFileService = productFileService;
        }




        /// Get all products that are not deleted
        public async Task<IEnumerable<ProductDetailsdto>> GetAllExistProductsAsync()
        {

            var allExistProducts = await _appDbContext.Products
                .Include(s => s.Stores)
                .Include(b => b.Brands)
                .Include(c => c.Category)
                .Where(p => p.IsExisit)
                .ToListAsync();

            var result = new List<ProductDetailsdto>();

            foreach (var product in allExistProducts)
            {
                var images = await _appDbContext.imageForProducts
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => i.ImageUrl)
                    .ToListAsync();
                // check if the product has stores
                var productStores = await _appDbContext.ProductStores
                    .Where(s => s.ProductId == product.ProductId)
                    .Select(s => new StoerQuantity
                    {
                        StoreId = s.StoreId,
                        Quantity = s.Quanttity
                    })
                    .ToListAsync();

                result.Add(new ProductDetailsdto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ModelYear = product.ModelYear,
                    ListPrice = product.ListPrice,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    IsExist = product.IsExisit,
                    ImageURLs = images,
                    Stores = productStores
                });
            }
            return result;
        }




        /// Get all products including deleted ones 
        public async Task<IEnumerable<ProductDetailsdto>> GetAllDeletedProductsAsync()
        {
            var allExistProducts = await _appDbContext.Products
               .Include(s => s.Stores)
               .Include(b => b.Brands)
               .Include(c => c.Category)
               .Where(p => p.IsExisit == false)
               .ToListAsync();

            var result = new List<ProductDetailsdto>();

            foreach (var product in allExistProducts)
            {
                var images = await _appDbContext.imageForProducts
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => i.ImageUrl)
                    .ToListAsync();
                // check if the product has stores
                var productStores = await _appDbContext.ProductStores
                    .Where(s => s.ProductId == product.ProductId)
                    .Select(s => new StoerQuantity
                    {
                        StoreId = s.StoreId,
                        Quantity = s.Quanttity
                    })
                    .ToListAsync();

                result.Add(new ProductDetailsdto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ModelYear = product.ModelYear,
                    ListPrice = product.ListPrice,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    IsExist = product.IsExisit,
                    ImageURLs = images,
                    Stores = productStores
                });
            }
            return result;
        }






        /// Get product by ID
        public async Task<ProductDetailsdto> GetProductByIdAsync(int productId)
        {
            var product = await _appDbContext.Products
                .Include(s => s.Stores).Include(b => b.Brands).Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.IsExisit);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            // get images for product
            var images = await _appDbContext.imageForProducts
                .Where(i => i.ProductId == productId)
                .Select(i => i.ImageUrl)
                .ToListAsync();

            // check if the product has stores
            var productStores = await _appDbContext.ProductStores
                .Where(s => s.ProductId == productId && s.Stores.IsExist)
                .Select(s => new StoerQuantity
                {
                    StoreId = s.StoreId,
                    Quantity = s.Quanttity
                })
                .ToListAsync();

            return new ProductDetailsdto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ModelYear = product.ModelYear,
                ListPrice = product.ListPrice,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                IsExist = product.IsExisit,
                ImageURLs = images,
                Stores = productStores
            };
        }





        /// Get product by name
        public async Task<IEnumerable<ProductDetailsdto>> GetProductByNameAsync(string productname)
        {
            var products = await _appDbContext.Products
                .Include(s => s.Stores).Include(b => b.Brands).Include(c => c.Category)
                .Where(p => p.ProductName == productname && p.IsExisit).ToListAsync();

            if (products == null || !products.Any())
                throw new KeyNotFoundException($"Product with name {productname} not found.");

            var result = new List<ProductDetailsdto>();

            foreach (var product in products)
            {
                var images = await _appDbContext.imageForProducts
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => i.ImageUrl)
                    .ToListAsync();
                // check if the product has stores
                var productStores = await _appDbContext.ProductStores
                    .Where(s => s.ProductId == product.ProductId && s.Stores.IsExist)
                    .Select(s => new StoerQuantity
                    {
                        StoreId = s.StoreId,
                        Quantity = s.Quanttity
                    })
                    .ToListAsync();

                result.Add(new ProductDetailsdto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ModelYear = product.ModelYear,
                    ListPrice = product.ListPrice,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    IsExist = product.IsExisit,
                    ImageURLs = images,
                    Stores = productStores
                });
            }
            return result;
        }





        /// Create a new product
        public async Task<Product> CreateProductAsync(AddProductToStore productdto)
        {
            var brand = await _appDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == productdto.BrandId && b.IsExist);
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == productdto.CategoryId && c.IsExsit);
            if (brand == null && category == null)
                throw new KeyNotFoundException("Invalid Category or Brand");

            var product = new Product
            {
                ProductName = productdto.ProductName,
                ModelYear = productdto.ModelYear,
                ListPrice = productdto.ListPrice,
                BrandId = productdto.BrandId,
                CategoryId = productdto.CategoryId,
                IsExisit = true
            };
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();


            return product;
        }






        /// Add image file to product
        public async Task SaveProductImageAsync(int id, string name, List<IFormFile> ImageFile)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id && p.IsExisit);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            bool isMainImage = true;
            var Images = new List<ImageForProduct>();

            int i = 1;

            foreach (var image in ImageFile)
            {
                var imageUrl = await _productFileService.SaveFile(image, $"{name}_{i}");

                Images.Add(new ImageForProduct
                {
                    ProductId = product.ProductId,
                    ImageUrl = imageUrl,
                    IsMainImage = isMainImage,
                    ImageName = $"{name} ({i})"
                });

                isMainImage = false; // بعد أول صورة
                i++;
            }

            _appDbContext.imageForProducts.AddRange(Images);
            await _appDbContext.SaveChangesAsync();

        }






        /// Update an existing product
        public async Task<Product> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var brand = await _appDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == updateProductDto.BrandId && b.IsExist);
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == updateProductDto.CategoryId && c.IsExsit);
            if (brand == null && category == null)
                throw new KeyNotFoundException("Invalid Category or Brand");

            // Check if the product exists in the database
            var existingProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            // Update the product properties
            existingProduct.ProductName = updateProductDto.ProductName;
            existingProduct.ModelYear = updateProductDto.ModelYear;
            existingProduct.ListPrice = updateProductDto.ListPrice;
            existingProduct.BrandId = updateProductDto.BrandId;
            existingProduct.CategoryId = updateProductDto.CategoryId;
            existingProduct.IsExisit = updateProductDto.IsExist;

            await _appDbContext.SaveChangesAsync();
            // Update the product stores
            var productStores = await _appDbContext.ProductStores
                .Where(ps => ps.ProductId == id)
                .ToListAsync();

            if (updateProductDto.Stores != null)
            {
                foreach (var store in updateProductDto.Stores)
                {
                    var existingStore = productStores.FirstOrDefault(ps => ps.StoreId == store.StoreId);
                    if (existingStore != null)
                    {
                        existingStore.Quanttity = store.Quantity;
                    }
                    else
                    {
                        var newProductStore = new ProductStore
                        {
                            ProductId = id,
                            StoreId = store.StoreId,
                            Quanttity = store.Quantity
                        };
                        await _appDbContext.ProductStores.AddAsync(newProductStore);
                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }
            return existingProduct;
        }







        /// Update product image

        public async Task UpdateProductImageAsync(int id, int ImageId)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id && p.IsExisit);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            var exsitImage = await _appDbContext.imageForProducts
                .Where(i => i.ProductId == id)
                .ToListAsync();

            if (!exsitImage.Any())
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            foreach (var imageFile in exsitImage)
            {
                imageFile.IsMainImage = false;
            }

            var images = exsitImage.FirstOrDefault(i => i.Id == ImageId);
            if (images == null)
                throw new KeyNotFoundException($"Images with id {ImageId} not found.");

            images.IsMainImage = true;

            await _appDbContext.SaveChangesAsync();
        }








        public async Task DeleteProductAsync(int id)
        {
            // Check if the product exists in the database
            var existingProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id && p.IsExisit);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            // Remove the product from the database            
            existingProduct.IsExisit = false;

            var productStores = await _appDbContext.ProductStores
                .Where(ps => ps.ProductId == id)
                .ToListAsync();
            foreach (var productStore in productStores)
            {
                _appDbContext.ProductStores.RemoveRange(productStore);
            }

            var images = await _appDbContext.imageForProducts
                    .Where(oi => oi.ProductId == id)
                    .ToListAsync();
            foreach (var image in images)
            {
                _productFileService.DeleteFile(image.ImageUrl);
            }
            _appDbContext.imageForProducts.RemoveRange(images);
            await _appDbContext.SaveChangesAsync();
        }







        // add images for products
        public async Task UploadImages(int productId, string name, List<IFormFile> images)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.IsExisit);
            if (product == null)
                throw new KeyNotFoundException($"Product with id {productId} not found");


            var existingImages = await _appDbContext.imageForProducts
                .Where(i => i.ProductId == productId)
                .ToListAsync();

            bool HasImage = await _appDbContext.imageForProducts.AnyAsync(i => i.ProductId == productId);

            var imageEntities = new List<ImageForProduct>();
            bool isMainImage = !HasImage;
            int i = existingImages.Count + 1;

            foreach (var image in images)
            {
                var imageUrl = await _productFileService.SaveFile(image, $"{name}_({i})");

                imageEntities.Add(new ImageForProduct
                {
                    ProductId = productId,
                    ImageUrl = imageUrl,
                    IsMainImage = isMainImage,
                    ImageName = $"{name} ({i})"
                });

                isMainImage = false; // بعد أول صورة
                i++;
            }

            _appDbContext.imageForProducts.AddRange(imageEntities);
            await _appDbContext.SaveChangesAsync();
        }







        public async Task DeleteImagesAsync(int productId, int ImageId)
        {

            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId && p.IsExisit);

            if (product == null)
                throw new ArgumentException("Invalid Product id.");

            var productImage = await _appDbContext.imageForProducts
                .FirstOrDefaultAsync(ip => ip.ProductId == productId && ip.Id == ImageId);

            if (productImage == null)
                throw new KeyNotFoundException($"Image with ID {ImageId} not found.");

            if (!productImage.IsMainImage)
            {
                _appDbContext.imageForProducts.Remove(productImage);
                await _appDbContext.SaveChangesAsync();

                _productFileService.DeleteFile(productImage.ImageUrl);
            }

            var mainImage = await _appDbContext.imageForProducts
                .Where(i => i.ProductId == productId && i.Id != ImageId)
                .ToListAsync();

            if (mainImage.Any())
            {
                var firstImage = mainImage.First();
                firstImage.IsMainImage = true;
                _appDbContext.imageForProducts.Update(firstImage);
            }

            _appDbContext.imageForProducts.Remove(productImage);
            await _appDbContext.SaveChangesAsync();

            _productFileService.DeleteFile(productImage.ImageUrl);

        }

        
    }
}
