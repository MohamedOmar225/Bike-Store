using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bike_store_2.Data
{
    public class ProductStoreConfigration : IEntityTypeConfiguration<ProductStore>
    {       
        public void Configure(EntityTypeBuilder<ProductStore> builder)
        {


            builder.HasKey(a => new { a.product_id , a.store_id });

            builder.Property(a => a.Quanttity)
                .HasColumnType("INT");


            builder.ToTable("ProductStores");
        }
    }
}
