using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Domain.Exceptions;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, EmptyResponse>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<EmptyResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
         
            await _userService.CreateUserAsync(request.Email, request.Name, request.Password);
            return new EmptyResponse();
        }
    }
}
