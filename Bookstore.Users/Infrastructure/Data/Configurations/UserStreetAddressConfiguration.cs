using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Users.Infrastructure.Data.Configurations;
internal class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
    public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
    {
        builder.ToTable(nameof(UserStreetAddress));
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(x => x.StreetAddress);
    }
}
