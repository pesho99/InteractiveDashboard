using InteractiveDashboard.Domain.Dtos;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Register
{
    public class RegisterUserCommand : IRequest<EmptyResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
