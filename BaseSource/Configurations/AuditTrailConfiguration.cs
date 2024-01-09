
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(address => address.User)
                .WithOne(user => user.Address)
                .HasForeignKey<Address>(address => address.UserId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(false);

        }
    }
}
