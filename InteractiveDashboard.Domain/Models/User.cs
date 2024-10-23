using Microsoft.AspNetCore.Identity;

namespace InteractiveDashboard.Domain.Models
{
    public class User : IdentityUser
    {
        public string Name {  get; set; } = string.Empty;
    }
}
