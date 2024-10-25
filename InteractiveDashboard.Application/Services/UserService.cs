using InteractiveDashboard.Domain.Exceptions;
using InteractiveDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace InteractiveDashboard.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public UserService(IUserManager userManager, IConfiguration configuration, IJwtService jwtService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task CreateUserAsync(string email, string name, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                throw new GeneralException("User with the same username already exists");
            }
            User user = new()
            {
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                Name = name,
            };
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new MultipleException(result.Errors.Select(e => e.Description).ToList());
            }
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new GeneralException("User or password wrong");
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new GeneralException("User or password wrong");
            }
            var token = await _jwtService.GetToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<User> GetUserAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }
    }
}
