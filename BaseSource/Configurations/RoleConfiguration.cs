
using System.Reflection.Emit;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(r => r.UpdatedAt).HasDefaultValueSql("getdate()");
        }
    }
}
