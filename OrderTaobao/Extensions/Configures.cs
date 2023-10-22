namespace Microsoft.Extensions
{
    public static class Configures
    {
        public static IServiceCollection AddConfigures(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            return services;
        }
    }
}
