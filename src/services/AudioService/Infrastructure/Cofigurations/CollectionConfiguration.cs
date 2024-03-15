namespace AudioService.Cofigurations
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.HasKey(collection => collection.Id);

            builder.Property(collection => collection.Id).HasColumnType("varchar")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired(true);

            builder.Property(collection => collection.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(collection => collection.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(collection => collection.Enable)
                            .HasDefaultValue(true)
                            .IsRequired(true);

            builder.HasMany(collection => collection.Albums)
                .WithOne(album => album.Collection)
                .HasForeignKey(collection => collection.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
