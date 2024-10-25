﻿using InteractiveDashboard.Application.Handlers.Register;
using InteractiveDashboard.Application.Services;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserService _userService;
        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string token = await _userService.GetTokenAsync(request.Email, request.Password);
            return new LoginResponse(token);
        }
    }
}
