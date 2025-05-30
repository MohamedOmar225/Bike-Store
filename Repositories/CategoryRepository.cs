using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Repositories
{




    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDetailsDTO>> GetAllExistingCategoriesAsync();
        Task<IEnumerable<CategoryDetailsDTO>> GetAllDeletedCategoriesAsync();
        Task<CategoryDetailsDTO?> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<CategoryDetailsDTO?>> GetCategoryByNameAsync(string CategoryName);
        Task<Category> CreateCategoryAsync(CategoryDTO categoryDTO);
        Task<Category> UpdateCategoryAsync(int id , CategoryUpdateDTO updateDTO);       
        Task DeleteCategoryAsync(int id);
    }



    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        
       



        public async Task<Category> CreateCategoryAsync(CategoryDTO categoryDTO)
        {

            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName,
                IsExsit = true
            };

            await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();
            return category;

        }

    

        public async Task<Category> UpdateCategoryAsync(int id ,CategoryUpdateDTO updateDTO)
        {

            // Check if the category exists in the database
            var existingCategory = await appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && c.IsExsit);
            if (existingCategory == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            
            existingCategory.CategoryName = updateDTO.CategoryName;
            existingCategory.IsExsit = updateDTO.IsExsit;
            
            appDbContext.Categories.Update(existingCategory);
            await appDbContext.SaveChangesAsync();

            return existingCategory;
        }





        public async Task<IEnumerable<CategoryDetailsDTO>> GetAllExistingCategoriesAsync()
        {
            var category = await appDbContext.Categories.Where(c => c.IsExsit).ToListAsync();

            var result = new List<CategoryDetailsDTO>();
            foreach (var cat in category)
            {
                var products = await appDbContext.Products
                   .Where(p => p.CategoryId == cat.CategoryId && p.IsExisit)
                   .Select(p => new CategoryProductDTO
                   {
                       ProductName = p.ProductName,
                       price = p.ListPrice,
                       ModelYear = p.ModelYear
                   }).ToListAsync();

                var store =await appDbContext.Stores
                    .Where(p => appDbContext.Products
                    .Any(c => c.CategoryId == cat.CategoryId && c.IsExisit && appDbContext.ProductStores
                    .Any(sp => sp.StoreId == p.StoreId && sp.ProductId == c.ProductId)))
                    .Select(p => new CategoryStoreDTO
                    {
                        StoreId = p.StoreId,
                        StoreName = p.StoreName
                    }).ToListAsync();

                var categoryDetails = new CategoryDetailsDTO
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    IsExsit = cat.IsExsit,
                    Products = products,
                    Stores = store
                };
                result.Add(categoryDetails);
            }
            return result;
        }




        public async Task<IEnumerable<CategoryDetailsDTO>> GetAllDeletedCategoriesAsync()
        {
            var category = await appDbContext.Categories.Where(c => !c.IsExsit).ToListAsync();

            var result = new List<CategoryDetailsDTO>();
            foreach (var cat in category)
            {
                var products = await appDbContext.Products
                   .Where(p => p.CategoryId == cat.CategoryId && p.IsExisit)
                   .Select(p => new CategoryProductDTO
                   {
                       ProductName = p.ProductName,
                       price = p.ListPrice,
                       ModelYear = p.ModelYear
                   }).ToListAsync();

                var store = await appDbContext.Stores
                    .Where(p => appDbContext.Products
                    .Any(c => c.CategoryId == cat.CategoryId && !c.IsExisit && appDbContext.ProductStores
                    .Any(sp => sp.StoreId == p.StoreId && sp.ProductId == c.ProductId)))
                    .Select(p => new CategoryStoreDTO
                    {
                        StoreId = p.StoreId,
                        StoreName = p.StoreName
                    }).ToListAsync();

                var categoryDetails = new CategoryDetailsDTO
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    IsExsit = cat.IsExsit,
                    Products = products,
                    Stores = store
                };
                result.Add(categoryDetails);
            }
            return result;
        }

        


        public async Task<CategoryDetailsDTO?> GetCategoryByIdAsync(int categoryId)
        {
            // Check if the category exists in the database
            var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId && c.IsExsit);

            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            var result = new List<CategoryDetailsDTO>();

            var products = await appDbContext.Products
                .Where(p => p.CategoryId == categoryId && p.IsExisit)
                .Select(p => new CategoryProductDTO
                {
                    ProductName = p.ProductName,
                    price = p.ListPrice,
                    ModelYear = p.ModelYear
                }).ToListAsync();

            var store = await appDbContext.Stores
                    .Where(p => appDbContext.Products
                    .Any(c => c.CategoryId == categoryId && c.IsExisit && appDbContext.ProductStores
                    .Any(sp => sp.StoreId == p.StoreId && sp.ProductId == c.ProductId)))
                    .Select(p => new CategoryStoreDTO
                    {
                        StoreId = p.StoreId,
                        StoreName = p.StoreName
                    }).ToListAsync();

            return new CategoryDetailsDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsExsit = category.IsExsit,
                Products = products,
                Stores = store
            };

        }







        public async Task<IEnumerable<CategoryDetailsDTO?>> GetCategoryByNameAsync(string CategoryName)
        {
            // Check if the category exists in the database
            var category = await appDbContext.Categories
                .Include(c => c.Products)
                .Where(c => c.CategoryName.ToLower() == CategoryName.ToLower() && c.IsExsit).ToListAsync();
            
            if (category == null || !category.Any())
                throw new KeyNotFoundException($"Category with name {CategoryName} not found.");

            var result = new List<CategoryDetailsDTO>();
            foreach (var cat in category)
            {
                var products = await appDbContext.Products
                   .Where(p => p.CategoryId == cat.CategoryId && p.IsExisit)
                   .Select(p => new CategoryProductDTO
                   {
                       ProductName = p.ProductName,
                       price = p.ListPrice,
                       ModelYear = p.ModelYear
                   }).ToListAsync();

                var store = await appDbContext.Stores
                    .Where(p => appDbContext.Products
                    .Any(c => c.CategoryId == cat.CategoryId && c.IsExisit && appDbContext.ProductStores
                    .Any(sp => sp.StoreId == p.StoreId && sp.ProductId == c.ProductId)))
                    .Select(p => new CategoryStoreDTO
                    {
                        StoreId = p.StoreId,
                        StoreName = p.StoreName
                    }).ToListAsync();

                var categoryDetails = new CategoryDetailsDTO
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    IsExsit = cat.IsExsit,
                    Products = products,
                    Stores = store
                };
                result.Add(categoryDetails);
            }
                return result;
        }









        public async Task DeleteCategoryAsync(int id)
        {
            var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && c.IsExsit);
            var products = await appDbContext.Products.Where(p => p.CategoryId == id && p.IsExisit).ToListAsync();

            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");

            if (products != null && products.Count > 0)
            {
                foreach (var product in products)
                {
                    product.IsExisit = false;
                    appDbContext.Products.Update(product);
                }
            }

            category.IsExsit = false;
            appDbContext.Categories.Update(category);
            await appDbContext.SaveChangesAsync();

        }

    
    }
}
