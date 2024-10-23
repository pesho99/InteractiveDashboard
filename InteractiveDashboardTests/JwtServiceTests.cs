using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Claims;

namespace InteractiveDashboardTests
{
    public class JwtServiceTests
    {
        JwtService _subject;
        Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c["JWT:ValidIssuer"]).Returns("TestIssuer");
            _configuration.Setup(c => c["JWT:ValidAudience"]).Returns("TestAudience");
            _configuration.Setup(c => c["JWT:Secret"]).Returns("secret");
            _configuration.Setup(c => c["JWT:ExpirationMinutes"]).Returns("60");
            _subject = new JwtService(_configuration.Object);
        }

        [Test]
        public async Task VerifyTokenGeneratedSuccessfully()
        {
            var user = new User { Email = "test@test.com", Name = "Test" };
            var token = await _subject.GetToken(user);

            Assert.That(token.Claims.Single(c => c.Type == ClaimTypes.Email).Value, Is.EqualTo("test@test.com"));
            //Add more verifications
        }
    }
}
