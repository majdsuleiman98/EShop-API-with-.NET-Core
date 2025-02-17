
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Config
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        void IEntityTypeConfiguration<AppUser>.Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(50).HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasMaxLength(50).HasColumnName("LastName");

            builder.HasOne(u => u.Address)
                   .WithOne()
                   .HasForeignKey<AppUser>(u => u.AddressId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
