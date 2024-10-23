using InteractiveDashboard.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace InteractiveDashboard.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<JwtSecurityToken> GetToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
