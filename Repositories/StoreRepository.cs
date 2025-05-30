using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.DTO.Store;
using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Repositories
{





    public interface IStoreRepository
    {
        Task<IEnumerable<StoreWithOrdersDto>> GetAllExistingStoresAsync();
        Task<IEnumerable<StoreWithOrdersDto>> GetAllDeletedStoresAsync();
        Task<StoreWithOrdersDto?> GetStoreByIdAsync(int storeId);
        Task<IEnumerable<StoreWithOrdersDto?>> GetStoreByNameAsync(string storeName);
        Task<Store> CreateStoreAsync(CreateStoreDTO createStore);
        Task<Store> UpdateStoreAsync(int id, UpdateStoreDto updateStore);
        Task DeleteStoreAsync(int DeleteStoreId, int alternativeStoreId);
        Task<AddProductToStoreDto> AddProductToStoreAsync(int id, AddProductToStoreDto addProductToStore);
    }



    public class StoreRepository : IStoreRepository
    {

        private readonly AppDbContext _appDbContext;
        public StoreRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public async Task<Store> CreateStoreAsync(CreateStoreDTO createStore)
        {
            var store = new Store
            {
                StoreName = createStore.StoreName,
                city = createStore.city,
                street = createStore.street,
                Phone = createStore.Phone,
                Email = createStore.Email,
                IsExist = true
            };
            await _appDbContext.Stores.AddAsync(store);
            await _appDbContext.SaveChangesAsync();
            return store;
        }





        public async Task<Store> UpdateStoreAsync(int id, UpdateStoreDto updateStore)
        {
            var store = await _appDbContext.Stores.FirstOrDefaultAsync(s => s.StoreId == id && s.IsExist);

            if (store == null)
                throw new KeyNotFoundException($"Store with ID {id} not found.");

            store.StoreName = updateStore.StoreName;
            store.city = updateStore.city;
            store.street = updateStore.street;
            store.Phone = updateStore.Phone;
            store.Email = updateStore.Email;
            store.IsExist = updateStore.IsExist;
            _appDbContext.Stores.Update(store);
            await _appDbContext.SaveChangesAsync();
            return store;
        }






        public async Task<IEnumerable<StoreWithOrdersDto>> GetAllExistingStoresAsync()
        {
            var stores = await _appDbContext.Stores.Include(s => s.Products)
                .Include(s => s.Orders).ThenInclude(oi => oi.OrderItems)
                .Include(o => o.Orders).ThenInclude(s => s.Employee)
                .Where(s => s.IsExist).ToListAsync();

            var result = new List<StoreWithOrdersDto>();
            foreach (var store in stores)
            {
                var storeWithOrdersDto = new StoreWithOrdersDto
                {
                    StoreId = store.StoreId,
                    StoreName = store.StoreName,
                    city = store.city,
                    street = store.street,
                    Phone = store.Phone,
                    Email = store.Email,
                    IsExist = store.IsExist,
                    Orders = store.Orders.Select(o => new OrderDetailsDto
                    {
                        OrderId = o.OrderId,
                        CustomertId = o.CustomerId,
                        EmployeeName = o.Employee.EmployeeName,
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                        ShippedDate = o.ShippedDate.ToString("yyyy-MM-dd"),
                        TotalAmount = o.TotalAmount,
                        Items = o.OrderItems.Select(i => new OrderItemDto
                        {
                            ProductId = i.ProductId,
                            Listprice = i.Listprice,
                            Quantity = i.Quantity,
                            Discount = i.Discount
                        }).ToList()
                    }).ToList()
                };
                result.Add(storeWithOrdersDto);
            }
            return result;
        }


       



        public async Task<IEnumerable<StoreWithOrdersDto>> GetAllDeletedStoresAsync()
        {
            var stores = await _appDbContext.Stores.Include(s => s.Products)
               .Include(s => s.Orders).ThenInclude(oi => oi.OrderItems)
               .Include(o => o.Orders).ThenInclude(s => s.Employee)
               .Where(s => !s.IsExist).ToListAsync();

            var result = new List<StoreWithOrdersDto>();
            foreach (var store in stores)
            {
                var storeWithOrdersDto = new StoreWithOrdersDto
                {
                    StoreId = store.StoreId,
                    StoreName = store.StoreName,
                    city = store.city,
                    street = store.street,
                    Phone = store.Phone,
                    Email = store.Email,
                    IsExist = store.IsExist,
                    Orders = store.Orders.Select(o => new OrderDetailsDto
                    {
                        OrderId = o.OrderId,
                        CustomertId = o.CustomerId,
                        EmployeeName = o.Employee.EmployeeName,
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                        ShippedDate = o.ShippedDate.ToString("yyyy-MM-dd"),
                        TotalAmount = o.TotalAmount,
                        Items = o.OrderItems.Select(i => new OrderItemDto
                        {
                            ProductId = i.ProductId,
                            Listprice = i.Listprice,
                            Quantity = i.Quantity,
                            Discount = i.Discount
                        }).ToList()
                    }).ToList()
                };
                result.Add(storeWithOrdersDto);
            }
            return result;
        }                           








        public async Task<StoreWithOrdersDto?> GetStoreByIdAsync(int storeId)
        {
            var store = await _appDbContext.Stores.Include(s => s.Products)
                .Include(s => s.Orders).ThenInclude(oi => oi.OrderItems)
                .Include(o => o.Orders).ThenInclude(s => s.Employee)
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.IsExist);

            if (store == null)
                throw new KeyNotFoundException($"Store with ID {storeId} not found.");

            var storeWithOrdersDto = new StoreWithOrdersDto
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                city = store.city,
                street = store.street,
                Phone = store.Phone,
                Email = store.Email,
                IsExist = store.IsExist,
                Orders = store.Orders.Select(o => new OrderDetailsDto
                {
                    OrderId = o.OrderId,
                    CustomertId = o.CustomerId,
                    EmployeeName = o.Employee.EmployeeName,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                    ShippedDate = o.ShippedDate.ToString("yyyy-MM-dd"),
                    TotalAmount = o.TotalAmount,
                    Items = o.OrderItems.Select(i => new OrderItemDto
                    {
                        ProductId = i.ProductId,
                        Listprice = i.Listprice,
                        Quantity = i.Quantity,
                        Discount = i.Discount
                    }).ToList()
                }).ToList()
            };
            return storeWithOrdersDto;

        }








        public async Task<IEnumerable<StoreWithOrdersDto?>> GetStoreByNameAsync(string storeName)
        {
            var stores = await _appDbContext.Stores.Include(s => s.Products)
                .Include(s => s.Orders).ThenInclude(oi => oi.OrderItems)
                .Include(o => o.Orders).ThenInclude(s => s.Employee)
                .Where(s => s.StoreName.ToLower() == storeName.ToLower() && s.IsExist).ToListAsync();

            if (stores == null)
                throw new KeyNotFoundException($"Store with Nmae {storeName} not found.");

            var result = new List<StoreWithOrdersDto>();
            foreach (var store in stores)
            {
                var storeWithOrdersDto = new StoreWithOrdersDto
                {
                    StoreId = store.StoreId,
                    StoreName = store.StoreName,
                    city = store.city,
                    street = store.street,
                    Phone = store.Phone,
                    Email = store.Email,
                    IsExist = store.IsExist,
                    Orders = store.Orders.Select(o => new OrderDetailsDto
                    {
                        OrderId = o.OrderId,
                        CustomertId = o.CustomerId,
                        EmployeeName = o.Employee.EmployeeName,
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                        ShippedDate = o.ShippedDate.ToString("yyyy-MM-dd"),
                        TotalAmount = o.TotalAmount,
                        Items = o.OrderItems.Select(i => new OrderItemDto
                        {
                            ProductId = i.ProductId,
                            Listprice = i.Listprice,
                            Quantity = i.Quantity,
                            Discount = i.Discount
                        }).ToList()
                    }).ToList()
                };
                result.Add(storeWithOrdersDto);
            }
            return result;
        }

      






        public async Task<AddProductToStoreDto> AddProductToStoreAsync(int id, AddProductToStoreDto addProductToStore)
        {
            var product = await _appDbContext.Products.
                FirstOrDefaultAsync(p => p.ProductId == id && p.IsExisit);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            var ProductToStore = new List<ProductStore>();

            foreach (var stoerQuantity in addProductToStore.stoerQuantities)
            {
                var stores = await _appDbContext.Stores.AnyAsync(s => s.StoreId == stoerQuantity.StoreId && s.IsExist);
                if (!stores)
                    throw new KeyNotFoundException($"Store with ID {stoerQuantity.StoreId} not found.");

                var isExist = await _appDbContext.ProductStores
                    .AnyAsync(ps => ps.ProductId == id && ps.StoreId == stoerQuantity.StoreId);

                if (isExist)
                    throw new ArgumentException($"Product with ID {product.ProductId} already exists in Store with ID {stoerQuantity.StoreId}.");

                ProductToStore.Add(new ProductStore
                {
                    ProductId = id,
                    StoreId = stoerQuantity.StoreId,
                    Quanttity = stoerQuantity.Quantity
                });
            }
            await _appDbContext.ProductStores.AddRangeAsync(ProductToStore);
            await _appDbContext.SaveChangesAsync();
            return addProductToStore;
        }


       



        public async Task DeleteStoreAsync(int DeleteStoreId, int alternativeStoreId)
        {
            var store = await _appDbContext.Stores.FirstOrDefaultAsync(s => s.StoreId == DeleteStoreId && s.IsExist);
            if (store == null)
                throw new KeyNotFoundException($"Store with ID {DeleteStoreId} not found.");

            var employees = await _appDbContext.Employees.Where(e => e.StoreId == DeleteStoreId && e.IsActive).ToListAsync();
            foreach ( var employee in employees)
            {
                employee.IsActive = false;
            }

            var productstore = await _appDbContext.ProductStores.Where(ps => ps.StoreId == DeleteStoreId).ToListAsync();
            foreach (var product in productstore)
            {
                var productStore = await _appDbContext.ProductStores
                    .FirstOrDefaultAsync(ps => ps.ProductId == product.ProductId && ps.StoreId == alternativeStoreId);
                if (productStore != null)
                {
                    productStore.Quanttity += product.Quanttity;
                }
                else
                {
                    var newProStore = new ProductStore
                    {
                        StoreId = alternativeStoreId,
                        ProductId = product.ProductId,
                        Quanttity = product.Quanttity,
                    };
                    _appDbContext.ProductStores.Add(newProStore);
                }
                _appDbContext.ProductStores.Remove(product);
            }

            store.IsExist = false;
            _appDbContext.Stores.Update(store);
            await _appDbContext.SaveChangesAsync();
        }



    }
}
