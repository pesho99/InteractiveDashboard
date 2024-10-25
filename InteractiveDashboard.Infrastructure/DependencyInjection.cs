using InteractiveDashboard.Application.Repos;
using InteractiveDashboard.Domain.Models;
using InteractiveDashboard.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveDashboard.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnStr");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.AddScoped<IPersonalTickerRepo, PersonalTickerRepo>();

            return services;
        }
        public static async Task InitializeInfrastructureServices(this IServiceProvider sp)
        {
            await DbInitializer.Initialize(sp);
        }
    }
}
