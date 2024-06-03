using Bookstore.OrderProcessing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.OrderProcessing.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
           .Property(x => x.Id)
           .ValueGeneratedNever();

        builder
            .Property(x => x.Description)
            .HasMaxLength(100)
            .IsRequired();
    }
}
