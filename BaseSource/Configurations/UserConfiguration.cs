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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
 /*           builder.HasMany(u => u.Notifications)
       
             .Use
             .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(n => n.Order)
            .WithOne(o => o.Notification)
             .HasForeignKey<Notification>(n => n.OrderID)
             .OnDelete(DeleteBehavior.ClientCascade);*/
        }
    }
   
}
