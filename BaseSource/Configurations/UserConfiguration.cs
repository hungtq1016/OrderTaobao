using BaseSource.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Each User can have many entries in the UserRole join table
            builder.HasMany(user => user.Roles)
                .WithOne(userrole => userrole.User)
                .HasForeignKey(userrole => userrole.UserId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            builder.HasMany(user => user.Orders)
                .WithOne(order => order.User)
                .HasForeignKey(order => order.UserId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            builder.HasMany(user => user.Images)
                .WithOne(imageuser => imageuser.User)
                .HasForeignKey(imageuser => imageuser.UserId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            builder.HasMany(user => user.AuditTrail)
                .WithOne(audit => audit.User)
                .HasForeignKey(audit => audit.UserId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(false);
        }
    }
   
}
