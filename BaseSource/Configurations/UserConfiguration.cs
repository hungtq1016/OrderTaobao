using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BaseSource.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Each User can have many entries in the UserRole join table
            builder.HasMany(user => user.Roles)
                .WithOne(userrole => userrole.User)
                .HasForeignKey(userrole => userrole.UserId)
                .IsRequired();

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
        }
    }
   
}
