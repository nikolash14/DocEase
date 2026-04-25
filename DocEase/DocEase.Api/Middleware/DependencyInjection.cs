using DocEase.Application.ServiceReference;
using DocEase.Infrastructure.IRepository;
using DocEase.Infrastructure.Repository;
using DocEase.Infrastructure.Service;

namespace DocEase.Api.Middleware
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
