using Bookstore.OrderProcessing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.OrderProcessing.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(x => x.ShippingAddress, address =>
        {
            address.Property(a => a.Street1)
                .HasMaxLength(Constants.STREET_MAXLENGTH);
            address.Property(a => a.Street2)
                .HasMaxLength(Constants.STREET_MAXLENGTH);
            address.Property(a => a.City)
                .HasMaxLength(Constants.CITY_MAXLENGTH);
            address.Property(a => a.State)
                .HasMaxLength(Constants.STATE_MAXLENGTH);
            address.Property(a => a.PostalCode)
                .HasMaxLength(Constants.POSTALCODE_MAXLENGTH);
            address.Property(a => a.Country)
                .HasMaxLength(Constants.COUNTRY_MAXLENGTH);
        });

        builder.ComplexProperty(x => x.BillingAddress, address =>
        {
            address.Property(a => a.Street1)
                .HasMaxLength(Constants.STREET_MAXLENGTH);
            address.Property(a => a.Street2)
                .HasMaxLength(Constants.STREET_MAXLENGTH);
            address.Property(a => a.City)
                .HasMaxLength(Constants.CITY_MAXLENGTH);
            address.Property(a => a.State)
                .HasMaxLength(Constants.STATE_MAXLENGTH);
            address.Property(a => a.PostalCode)
                .HasMaxLength(Constants.POSTALCODE_MAXLENGTH);
            address.Property(a => a.Country)
                .HasMaxLength(Constants.COUNTRY_MAXLENGTH);
        });
    }
}
