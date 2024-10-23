using InteractiveDashboard.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace InteractiveDashboard.Application.Services
{
    public interface IJwtService
    {
        Task<JwtSecurityToken> GetToken(User user);
    }
}