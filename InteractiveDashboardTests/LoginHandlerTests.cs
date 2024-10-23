using InteractiveDashboard.Application.Handlers.Login;
using InteractiveDashboard.Application.Handlers.Register;
using InteractiveDashboard.Application.Services;
using Moq;

namespace InteractiveDashboardTests
{
    public class LoginHandlerTests
    {
        private LoginHandler _subject;
        private Mock<IUserService> _userService;

        public void Setup()
        {
            _subject = new LoginHandler(_userService.Object);
        }

        public async Task TestCallsGetToken()
        {
            var request = new LoginCommand
            {
                Email = "test@test.com",
                Password = "password",
            };
            var cs = new CancellationTokenSource();
            await _subject.Handle(request, cs.Token);

            _userService.Verify(m => m.GetToken(request.Email, request.Password), Times.Once);
        }
    }]
}
