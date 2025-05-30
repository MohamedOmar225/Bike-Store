using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bike_store_2.Repositories
{


    public interface IBrandRepository
    {
        Task<IEnumerable<BrandDetailsDTO>> GetAllExistingBrandsAsync();
        Task<IEnumerable<BrandDetailsDTO>> GetAllDeletedBrandsAsync();
        Task<BrandDetailsDTO?> GetBrandByIdAsync(int brandId);
        Task<IEnumerable<BrandDetailsDTO?>> GetBrandByNameAsync(string brandname);
        Task<Brand> CreateBrandAsync(CreateBrandDto createBrand);
        Task<Brand> UpdateBrandAsync(int id, UpdateBrandDto updateBrand);        
        Task DeleteBrandAsync(int id);
    }







    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _appDbContext;

        public BrandRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

       


        // create brand
        public async Task<Brand> CreateBrandAsync(CreateBrandDto createBrand)
        {
            var brand = new Brand
            {
                BrandName = createBrand.BrandName,
                IsExist = true
            };
            await _appDbContext.Brands.AddAsync(brand);
            await _appDbContext.SaveChangesAsync();
            return brand;
        }



        // update brand
        public async Task<Brand> UpdateBrandAsync(int id, UpdateBrandDto updateBrand)
        {
            var existingBrand = await _appDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == id && b.IsExist);
            if (existingBrand == null)
                throw new KeyNotFoundException($"Brand with ID {id} not found.");

            existingBrand.BrandName = updateBrand.BrandName;
            existingBrand.IsExist = updateBrand.IsExist;
            _appDbContext.Brands.Update(existingBrand);
            await _appDbContext.SaveChangesAsync();
            return existingBrand;
        }





        public async Task<IEnumerable<BrandDetailsDTO>> GetAllExistingBrandsAsync()
        {
            var brands = await _appDbContext.Brands.Where(b => b.IsExist).ToListAsync();

            var result = new List<BrandDetailsDTO>();
            foreach (var brand in brands)
            {
                var products = await _appDbContext.Products
                    .Where(p => p.BrandId == brand.BrandId && p.IsExisit)
                    .Select(p => new ProductDTO
                    {
                        ProductName = p.ProductName,
                        price = p.ListPrice,
                        ModelYear = p.ModelYear
                    }).ToListAsync();

                var stores = await _appDbContext.Stores
                    .Where(s => _appDbContext.Products
                    .Any(p => p.BrandId == brand.BrandId && p.IsExisit && _appDbContext.ProductStores
                    .Any(sp => sp.StoreId == s.StoreId && sp.ProductId == p.ProductId)))
                    .Select(s => new StoreDTO
                    {
                        StoreId = s.StoreId,
                        StoreName = s.StoreName,
                    }).ToListAsync();

                var brandDetails = new BrandDetailsDTO
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    IsExist = brand.IsExist,
                    Products = products,
                    Stores = stores
                };
                result.Add(brandDetails);
            }
            return result;
        }



        // get all deleted brands
        public async Task<IEnumerable<BrandDetailsDTO>> GetAllDeletedBrandsAsync()
        {
            var brands = await _appDbContext.Brands.Where(b => !b.IsExist).ToListAsync();

            var result = new List<BrandDetailsDTO>();
            foreach (var brand in brands)
            {
                var products = await _appDbContext.Products
                    .Where(p => p.BrandId == brand.BrandId && p.IsExisit)
                    .Select(p => new ProductDTO
                    {
                        ProductName = p.ProductName,
                        price = p.ListPrice,
                        ModelYear = p.ModelYear
                    }).ToListAsync();

                var stores = await _appDbContext.Stores
                    .Where(s => _appDbContext.Products
                    .Any(p => p.BrandId == brand.BrandId && !p.IsExisit && _appDbContext.ProductStores
                    .Any(sp => sp.StoreId == s.StoreId && sp.ProductId == p.ProductId)))
                    .Select(s => new StoreDTO
                    {
                        StoreId = s.StoreId,
                        StoreName = s.StoreName,
                    }).ToListAsync();

                var brandDetails = new BrandDetailsDTO
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    IsExist = brand.IsExist,
                    Products = products,
                    Stores = stores
                };
                result.Add(brandDetails);
            }
            return result;
        }

        

        public async Task<BrandDetailsDTO?> GetBrandByIdAsync(int brandId)
        {
            var brand = await _appDbContext.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.BrandId == brandId && b.IsExist);

            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {brandId} not found.");

            var result = new List<BrandDetailsDTO>();
           
            var products = await _appDbContext.Products
                .Where(p => p.BrandId == brand.BrandId && p.IsExisit)
                .Select(p => new ProductDTO
                {
                    ProductName = p.ProductName,
                    price = p.ListPrice,
                    ModelYear = p.ModelYear
                }).ToListAsync();

            var stores = await _appDbContext.Stores
                    .Where(s => _appDbContext.Products
                    .Any(p => p.BrandId == brand.BrandId && p.IsExisit && _appDbContext.ProductStores
                    .Any(sp => sp.StoreId == s.StoreId && sp.ProductId == p.ProductId)))
                    .Select(s => new StoreDTO
                    {
                        StoreId = s.StoreId,
                        StoreName = s.StoreName,
                    }).ToListAsync();

            return new BrandDetailsDTO
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                IsExist = brand.IsExist,
                Products = products,
                Stores = stores
            };           

        }




        public async Task<IEnumerable<BrandDetailsDTO?>> GetBrandByNameAsync(string brandName)
        {

            var brands = await _appDbContext.Brands
                .Include(b => b.Products)
                .Where(b => b.BrandName.ToLower() == brandName.ToLower() && b.IsExist).ToListAsync();

            if (brands == null || !brands.Any())
                throw new KeyNotFoundException($"Brand with name {brandName} not found.");

            var result = new List<BrandDetailsDTO>();
            foreach (var brand in brands)
            {
                var products = await _appDbContext.Products
                   .Where(p => p.BrandId == brand.BrandId && p.IsExisit)
                   .Select(p => new ProductDTO
                   {
                       ProductName = p.ProductName,
                       price = p.ListPrice,
                       ModelYear = p.ModelYear
                   }).ToListAsync();

                var stores = await _appDbContext.Stores
                    .Where(s => _appDbContext.Products
                    .Any(p => p.BrandId == brand.BrandId && p.IsExisit && _appDbContext.ProductStores
                    .Any(sp => sp.StoreId == s.StoreId && sp.ProductId == p.ProductId)))
                    .Select(s => new StoreDTO
                    {
                        StoreId = s.StoreId,
                        StoreName = s.StoreName,
                    }).ToListAsync();

                var brandDetails = new BrandDetailsDTO
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    IsExist = brand.IsExist,
                    Products = products,
                    Stores = stores
                };
                result.Add(brandDetails);
            }
            return result;

        }




        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _appDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == id && b.IsExist);
            var products = await _appDbContext.Products.Where(p => p.BrandId == id).ToListAsync();

            if (brand == null)
                throw new KeyNotFoundException($"Brand with ID {id} not found.");

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    product.IsExisit = false;
                }
            }
            brand.IsExist = false;
            _appDbContext.Brands.Update(brand);
            await _appDbContext.SaveChangesAsync();

        }




      
       
    }
}
