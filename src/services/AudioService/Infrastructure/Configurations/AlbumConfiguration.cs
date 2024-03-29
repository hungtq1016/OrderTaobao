﻿namespace AudioService.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(album => album.Id);

            builder.Property(album => album.Id).HasColumnType("varchar")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired(true);

            builder.Property(album => album.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(album => album.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql(Constants.DateTimeAlgorithm)
                .IsRequired(true);

            builder.Property(album => album.Enable)
                .HasDefaultValue(true)
                .IsRequired(true);

            builder.HasOne(album => album.Audio) 
                .WithMany() 
                .HasForeignKey(album => album.AudioId) 
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(album => album.Collection)
                .WithMany()
                .HasForeignKey(album => album.CollectionId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
