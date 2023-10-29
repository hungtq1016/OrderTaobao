
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasMany(district => district.Wards)
                .WithOne(address => address.District)
                .HasForeignKey(address => address.DistrictId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(true);

            builder.HasOne(district => district.Province)
                .WithMany(province => province.Districts)
                .HasForeignKey(district => district.ProvinceId)
                .IsRequired(true);

            builder.HasOne(district => district.AdministrativeUnit)
                .WithMany(au => au.Districts)
                .HasForeignKey(district => district.AdministrativeUnitID).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(true);
        }
    }
}
