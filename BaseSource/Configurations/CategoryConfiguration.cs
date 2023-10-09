
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasOne(c => c.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(c => c.UpdatedAt).HasDefaultValueSql("getdate()");
            builder.Navigation(x => x.Parent)
            .IsRequired(false);
        }
    }
}
