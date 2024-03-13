namespace AudioService.Infrastructure
{
    public class AudioRepository<TEntity> : RepositoryBase<AudioContext, TEntity> where TEntity : Entity
    {
        public AudioRepository(AudioContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}
