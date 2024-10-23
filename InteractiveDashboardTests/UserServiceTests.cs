using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Domain.Exceptions;
using InteractiveDashboard.Domain.Models;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace InteractiveDashboardTests
{
    public class UserServiceTests
    {
        UserService _subject;
        Mock<IUserManager> _managerMock = new Mock<IUserManager>();
        Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();

        [SetUp]
        public void Setup()
        {
            _subject = new UserService(_managerMock.Object, _jwtServiceMock.Object);
        }

        [Test]
        public async Task Verify_ManagerMock_CalledOnCreate()
        {
            await _subject.CreateUserAsync("test@test.com", "user", "password");

            _managerMock.Verify(x => x.CreateAsync(It.Is<User>(u => u.Name == "user" && u.Email == "test@test.com"), "password"), Times.Once);
        }

        [Test]
        public void Verify_ThrowsError_OnCreate_IfUserExists()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new User()));
            Assert.ThrowsAsync<GeneralException>(async () => await _subject.CreateUserAsync("test@test.com", "test", "password"));
            _managerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Verify_CallsGetUser_OnGetUser()
        {
            await _subject.GetUser("test@test.com");
            _managerMock.Verify(x => x.FindByEmailAsync("test@test.com"), Times.Once);
        }

        [Test]
        public async Task Verify_CallsGetEmail_OnGetUser()
        {
            await _subject.GetUser("test@test.com");
            _managerMock.Verify(x => x.FindByEmailAsync("test@test.com"), Times.Once);
        }

        public async Task Verify_ThrowsError_OnGetToken_WhenNoUser()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult((User)null));
            Assert.ThrowsAsync<GeneralException>(async () => await _subject.GetToken("test@test.com", "password"));
        }
        public async Task Verify_ThrowsError_OnGetToken_WhenPasswordIsWrong()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new User()));
            _managerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);
            Assert.ThrowsAsync<GeneralException>(async () => await _subject.GetToken("test@test.com", "password"));
        }
        public async Task Verify_ReturnsToken_OnGetToken_Ok()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new User()));
            _managerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
            var token = new JwtSecurityToken();
            await _subject.GetToken("test@test.com", "password");
        }
    }
}
