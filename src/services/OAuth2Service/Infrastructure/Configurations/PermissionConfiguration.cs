namespace OAuth2Service.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(permission => permission.Id);

            builder.Property(permission => permission.Id).HasColumnType("varchar")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired(true);

            builder.Property(permission => permission.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(permission => permission.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(permission => permission.Enable)
                .HasDefaultValue(true)
                .IsRequired(true);

            builder.HasMany(permission => permission.Assignments)
                .WithOne(assign => assign.Permission)
                .HasForeignKey(assign => assign.PermissionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
