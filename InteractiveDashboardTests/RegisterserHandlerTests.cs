using InteractiveDashboard.Application.Handlers.Register;
using InteractiveDashboard.Application.Services;
using Moq;

namespace InteractiveDashboardTests
{
    public class RegisterserHandlerTests
    {
        private RegisterUserHandler _subject;
        private Mock<IUserService> _userService;

        public void Setup()
        {
            _subject = new RegisterUserHandler(_userService.Object);
        }

        public async Task TestCallsGetToken()
        {
            var request = new RegisterUserCommand
            {
                Name = "test",
                Email = "test@test.com",
                Password = "password",
            };
            var cs = new CancellationTokenSource();
            await _subject.Handle(request, cs.Token);

            _userService.Verify(m => m.CreateUserAsync(request.Name, request.Email, request.Password), Times.Once);
        }
    }
}
