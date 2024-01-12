
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            /*builder.Property(roleclaim => roleclaim.Id)
                .HasColumnName("ID")
                .HasColumnType("VARCHAR")
                .HasMaxLength(36)
                .HasDefaultValueSql("NEWID()");*/
        }
    }
}
