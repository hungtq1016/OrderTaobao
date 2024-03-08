namespace OAuth2Service.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).HasColumnType("varchar")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired(true);

            builder.Property(user => user.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(user => user.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(user => user.Enable)
                .HasDefaultValue(true)
                .IsRequired(true);

            builder.Property(user => user.ImageId).HasColumnType("varchar")
                .HasMaxLength(36)
                .IsRequired(false);

            builder.Property(user => user.Password)
                .HasDefaultValue(BCrypt.Net.BCrypt.HashPassword("Th1s1sP@ssword"))
                .IsRequired(false);

            builder.HasMany(user => user.Groups)
                .WithOne(group => group.User)
                .HasForeignKey(group => group.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
