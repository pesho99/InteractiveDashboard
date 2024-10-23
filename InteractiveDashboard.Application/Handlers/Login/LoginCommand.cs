using InteractiveDashboard.Application.Handlers.Login;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Register
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
