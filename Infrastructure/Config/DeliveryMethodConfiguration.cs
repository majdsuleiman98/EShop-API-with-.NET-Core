

using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        void IEntityTypeConfiguration<DeliveryMethod>.Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)").HasAnnotation("MinValue", 0.1);
            builder.Property(x => x.ShortName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DeliveryTime).IsRequired().HasMaxLength(50);
        }
    }
}
