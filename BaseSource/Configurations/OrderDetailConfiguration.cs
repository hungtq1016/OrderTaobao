
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(d => d.Order)
            .WithMany(o => o.Details)
             .HasForeignKey(d => d.OrderId)
             .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(d => d.Product)
            .WithMany(p => p.Orders)
             .HasForeignKey(n => n.ProductId)
             .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
