using bike_store_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bike_store_2.Data
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {


            builder.HasKey(a => new { a.Item_id, a.Order_id });

            builder.Property(t => t.Total)
                .HasComputedColumnSql("[Quantity] * [List_price] - [Discount]");
                

            builder.ToTable("OrderItems");
        }
    }
}
