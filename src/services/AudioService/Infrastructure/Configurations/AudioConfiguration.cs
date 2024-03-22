namespace AudioService.Configurations
{
    public class AudioConfiguration : IEntityTypeConfiguration<Audio>
    {
        public void Configure(EntityTypeBuilder<Audio> builder)
        {
            builder.HasKey(audio => audio.Id);

            builder.Property(audio => audio.Id)
                .HasColumnType("varchar(36)")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired();

            builder.Property(audio => audio.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired();

            builder.Property(audio => audio.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired();

            builder.Property(audio => audio.Enable)
                .HasDefaultValue(true)
                .IsRequired(true);

            builder.HasMany(audio => audio.Albums)
                .WithOne(audio => audio.Audio)
                .HasForeignKey(audio => audio.AudioId)
                .IsRequired(false);
        }
    }
}
