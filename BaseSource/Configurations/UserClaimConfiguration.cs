
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
           /* builder.Property(roleclaim => roleclaim.Id)
                .HasColumnName("ID")
                .HasColumnType("VARCHAR")
                .HasMaxLength(36)
                .HasDefaultValueSql("NEWID()");*/
        }
    }
}
