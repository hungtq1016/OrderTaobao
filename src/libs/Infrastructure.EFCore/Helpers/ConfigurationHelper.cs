namespace Infrastructure.EFCore.Helpers
{
    public class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration(string path = null)
        {
            path ??= Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .Build();

            return builder;
        }
    }
}
