namespace AudioService.Cofigurations
{
    public class AudioConfiguration : IEntityTypeConfiguration<Audio>
    {
        public void Configure(EntityTypeBuilder<Audio> builder)
        {
            builder.HasKey(audio => audio.Id);

            builder.Property(audio => audio.Id).HasColumnType("varchar")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired(true);

            builder.Property(audio => audio.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(audio => audio.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(audio => audio.Enable)
                .HasDefaultValue(true)
                .IsRequired(true);

            builder.HasMany(audio => audio.Albums)
                .WithOne(album => album.Audio)
                .HasForeignKey(audio => audio.AudioId)
                .OnDelete(DeleteBehavior.ClientSetNull);            
        }
    }
}
