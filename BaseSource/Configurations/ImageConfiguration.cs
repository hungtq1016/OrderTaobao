using BaseSource.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            // Each User can have many entries in the UserRole join table
            builder.HasMany(image => image.Users)
                .WithOne(imageuser => imageuser.Image)
                .HasForeignKey(imageuser => imageuser.ImageId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

        }
    }

}
