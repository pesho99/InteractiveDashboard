using InteractiveDashboard.Domain.Models;

namespace InteractiveDashboard.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService; 

        public UserService(IUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public Task CreateUserAsync(string email, string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetToken(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
