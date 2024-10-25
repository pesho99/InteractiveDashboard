using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Domain.Exceptions;
using InteractiveDashboard.Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace InteractiveDashboardTests
{
    public class UserServiceTests
    {
        UserService _subject;
        Mock<IUserManager> _managerMock;
        Mock<IJwtService> _jwtServiceMock;
        Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _managerMock = new Mock<IUserManager>();
            _jwtServiceMock = new Mock<IJwtService>();
            _configuration = new Mock<IConfiguration>();
            _subject = new UserService(_managerMock.Object, _configuration.Object, _jwtServiceMock.Object);
        }

        [Test]
        public async Task Verify_ManagerMock_CalledOnCreate()
        {
            _managerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(Microsoft.AspNetCore.Identity.IdentityResult.Success);
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
            await _subject.GetUserAsync("test@test.com");
            _managerMock.Verify(x => x.FindByEmailAsync("test@test.com"), Times.Once);
        }

        [Test]
        public async Task Verify_CallsGetEmail_OnGetUser()
        {
            await _subject.GetUserAsync("test@test.com");
            _managerMock.Verify(x => x.FindByEmailAsync("test@test.com"), Times.Once);
        }
        [Test]

        public async Task Verify_ThrowsError_OnGetToken_WhenNoUser()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult((User)null));
            Assert.ThrowsAsync<GeneralException>(async () => await _subject.GetTokenAsync("test@test.com", "password"));
        }
        [Test]

        public async Task Verify_ThrowsError_OnGetToken_WhenPasswordIsWrong()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new User()));
            _managerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);
            Assert.ThrowsAsync<GeneralException>(async () => await _subject.GetTokenAsync("test@test.com", "password"));
        }
        [Test]

        public async Task Verify_ReturnsToken_OnGetToken_Ok()
        {
            _managerMock.Setup(m => m.FindByEmailAsync("test@test.com")).Returns(Task.FromResult(new User()));
            _managerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
            var token = new JwtSecurityToken();
            await _subject.GetTokenAsync("test@test.com", "password");
        }
    }
}
