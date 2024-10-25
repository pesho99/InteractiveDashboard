using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InteractiveDashboard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<ITickerService, TickerService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddHostedService<PriceTicker>();

            services.Configure<PriceTickerSetttings>(
                configuration.GetSection("PriceTicker"));
            return services;
        }
    }
}
