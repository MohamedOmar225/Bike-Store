using bike_store_2.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;




namespace bike_store_2.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> //DbContext    
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
      
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Employee> Employees  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Store> Stores  { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } 
        public DbSet<ProductStore> ProductStores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ImageForProduct> imageForProducts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            // اول طريقه بعملها لكل model عندي
            //modelBuilder.ApplyConfiguration(new CourseConfigration());

            // تاني طريقه اسهل واحسن
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // يروح يدور في ال  assemble  ويضيفهم تلقائي
        }


    }

    
}
