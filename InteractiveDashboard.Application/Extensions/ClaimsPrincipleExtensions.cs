using System.Security.Claims;

namespace InteractiveDashboard.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal cp)
        {
            return cp.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        }
    }
}
