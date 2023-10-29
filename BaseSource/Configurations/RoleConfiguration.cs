using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(role => role.Users)
                 .WithOne(userrole => userrole.Role)
                 .HasForeignKey(userrole => userrole.RoleId).OnDelete(DeleteBehavior.ClientCascade)
                 .IsRequired();
        }
    }
}
