
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.Customer)
            .WithOne(c => c.Notification)
             .HasForeignKey<Notification>(n => n.CustomerId)
             .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(n => n.Order)
            .WithOne(o => o.Notification)
             .HasForeignKey<Notification>(n => n.OrderID)
             .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
