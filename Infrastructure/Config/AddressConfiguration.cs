
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        void IEntityTypeConfiguration<Address>.Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Line).IsRequired().HasMaxLength(200);
            builder.Property(x => x.City).IsRequired().HasMaxLength(50);
            builder.Property(x => x.State).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Zipcode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Country).IsRequired().HasMaxLength(50);
        }
    }
}
