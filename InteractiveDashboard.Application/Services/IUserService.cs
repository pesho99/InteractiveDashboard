using InteractiveDashboard.Domain.Models;

namespace InteractiveDashboard.Application.Services
{
    public interface IUserService
    {
        Task CreateUser(string email, string name, string password);
        Task<string> GetToken(string email, string password);
        Task<User> GetUser(string email);
    }
}