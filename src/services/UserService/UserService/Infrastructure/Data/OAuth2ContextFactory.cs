namespace OAuth2Service.Infrastructure.Data
{
    public class OAuth2ContextFactory : AppDbContextFactory<OAuth2Context>
    {
        public OAuth2ContextFactory() : base("oauth2DB")
        {
        }
    }
}
