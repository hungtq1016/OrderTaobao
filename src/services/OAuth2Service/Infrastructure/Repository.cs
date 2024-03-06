namespace OAuth2Service.Repository
{
    public class OAuth2Repository<TEntity> : RepositoryBase<OAuth2Context, TEntity> where TEntity : Entity
    {
        public OAuth2Repository(OAuth2Context context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}
