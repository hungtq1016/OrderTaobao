namespace OAuth2Service.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Models.Token>
    {
        public void Configure(EntityTypeBuilder<Models.Token> builder)
        {
            builder.HasKey(token => token.Id);

            builder.Property(token => token.Id).HasColumnType("varchar")
                .HasMaxLength(36)
                .HasDefaultValueSql(Constants.UuidAlgorithm)
                .IsRequired(true);
        }

    }
}
