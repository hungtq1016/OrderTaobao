
using BaseSource.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Notification)
            .WithOne(n => n.Order)
             .HasForeignKey<Notification>(n => n.OrderID)
             .OnDelete(DeleteBehavior.ClientCascade);

            _ = builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
             .HasForeignKey(o => o.UserId)
             .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
