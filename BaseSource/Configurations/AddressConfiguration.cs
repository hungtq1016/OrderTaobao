
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSource.Configurations
{
    public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrailHistory>
    {
        public void Configure(EntityTypeBuilder<AuditTrailHistory> builder)
        {
            builder.HasOne(audit => audit.User)
                .WithMany(user => user.AuditTrail)
                .HasForeignKey(audit => audit.UserId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(false);

        }
    }
}
