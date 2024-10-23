using InteractiveDashboard.Domain.Models;

namespace InteractiveDashboard.Application.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(string email, string name, string password);
        Task<string> GetToken(string email, string password);
        Task<User> GetUser(string email);
    }
}