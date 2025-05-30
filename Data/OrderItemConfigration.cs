using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bike_store_2.Data
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {


            builder.HasKey(a => new { a.OrderId, a.ProductId });
           

            builder.Property(t => t.Listprice)
                .HasPrecision(10, 2);

            builder.Property(t => t.Quantity)
                .HasPrecision(10, 2);

            builder.Property(t => t.Discount)
                .HasPrecision(10, 2);
            

            builder.ToTable("OrderItems");
        }
    }
}
