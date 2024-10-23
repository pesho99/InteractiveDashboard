using InteractiveDashboard.Domain.Constants;
using InteractiveDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveDashboard.Infrastructure.Database
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            try
            {
                using (var scope = services.CreateScope())
                {
                    var sp = scope.ServiceProvider;

                    using (var context = sp.GetRequiredService<ApplicationContext>())
                    {
                        context.Database.Migrate();
                        var rolem = sp.GetRequiredService<RoleManager<IdentityRole>>();
                        if (!rolem.Roles.Any())
                        {
                            await rolem.CreateAsync(new IdentityRole(Roles.User));
                            await rolem.CreateAsync(new IdentityRole(Roles.Admin));
                        }
                        using var um = sp.GetRequiredService<UserManager<User>>();
                        if (!um.Users.Any())
                        {
                            User user = new()
                            {
                                Email = "admin@admin.com",
                                SecurityStamp = Guid.NewGuid().ToString(),
                                UserName = Guid.NewGuid().ToString(),
                                Name = "admin",
                            };
                            await um.CreateAsync(user, "Admin123!");
                            await um.AddToRoleAsync(user, Roles.Admin);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
