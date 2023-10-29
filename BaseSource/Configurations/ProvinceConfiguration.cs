
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasMany(province => province.Districts)
                .WithOne(district => district.Province)
                .HasForeignKey(district => district.ProvinceId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(true);

            builder.HasOne(province => province.AdministrativeUnit)
                .WithMany(au => au.Provinces)
                .HasForeignKey(province => province.AdministrativeUnitID).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(true);
        }
    }
}
