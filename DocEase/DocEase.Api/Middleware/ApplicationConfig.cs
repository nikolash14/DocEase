using DocEase.Api.Config.Model;

namespace DocEase.Api.Middleware
{
    public static class ApplicationConfig
    {
        public static void LoadStaticConfigFiles(
            this IConfigurationBuilder builder)
        {
            builder.AddJsonFile("Config/JSON/docEaseConfiguration.json", optional: false, reloadOnChange: true);
        }
        public static void LoadConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SqlSetting>(configuration.GetSection("ConnectionStrings"));
            services.Configure<SerilogSetting>(configuration.GetSection("Serilog"));
        }
    }
}
