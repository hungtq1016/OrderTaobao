
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.HasMany(ward => ward.Address)
                .WithOne(address => address.Ward)
                .HasForeignKey(address => address.WardId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(false);

            builder.HasOne(ward => ward.District)
                .WithMany(district => district.Wards)
                .HasForeignKey(ward => ward.DistrictId)
                .IsRequired(true);

            builder.HasOne(ward => ward.AdministrativeUnit)
                .WithMany(au => au.Wards)
                .HasForeignKey(ward => ward.AdministrativeUnitID).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(true);
        }
    }
}
