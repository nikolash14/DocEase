using DocEase.Application.Config;
using DocEase.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace DocEase.Api.Middleware
{
    public static class ApplicationConfig
    {
        public static void LoadStaticConfigFiles(
            this IConfigurationBuilder builder)
        {
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
        public static void LoadConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SqlSetting>(configuration.GetSection("ConnectionStrings"));
            services.Configure<SerilogSetting>(configuration.GetSection("SerilogLoggingConfig"));
            services.Configure<JwtSetting>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<DocEaseDbContext>(options =>
                                         options.UseSqlServer(configuration.GetConnectionString("SqlServerPrimary")),
                                         ServiceLifetime.Transient);
        }
    }
}
