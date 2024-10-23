using InteractiveDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace InteractiveDashboard.Application.Services
{
    public interface IUserManager
    {
        Task<IdentityResult> AddToRoleAsync(User user1, string role);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<User> FindByEmailAsync(string email);
    }
}