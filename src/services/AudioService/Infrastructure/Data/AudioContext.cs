namespace ImageService.Data
{
    public class AudioContext : AppDbContext
    {
        public AudioContext(DbContextOptions options) : base(options) { }
        public DbSet<Audio> Audios => Set<Audio>();
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Collection> Collections => Set<Collection>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration( new AudioConfiguration());
            modelBuilder.ApplyConfiguration( new AlbumConfiguration());
            modelBuilder.ApplyConfiguration( new CollectionConfiguration());
        }
    }
}
