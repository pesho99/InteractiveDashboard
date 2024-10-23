using InteractiveDashboard.Application.Handlers.Register;
using InteractiveDashboard.Application.Validators;

namespace InteractiveDashboardTests
{
    public class RegisterUserValidatorTests
    {
        RegisterUserValidator _subejct = new RegisterUserValidator();

        [Test]
        public void VerifyNoError_IfCorrect()
        {
            var regUser = new RegisterUserCommand
            {
                Name = "Test",
                Email = "test@test.com",
                Password = "Passw0rd!1"
            };
            var result = _subejct.Validate(regUser);
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void VerifyError_OnMissingName()
        {
            var regUser = new RegisterUserCommand
            {
                Name = "",
                Email = "test@test.com",
                Password = "Passw0rd!1"
            };
            var result = _subejct.Validate(regUser);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().PropertyName == "Name");
        }
        [Test]
        public void VerifyError_OnWrongEmail()
        {
            var regUser = new RegisterUserCommand
            {
                Name = "Test",
                Email = "test.com",
                Password = "Passw0rd!1"
            };
            var result = _subejct.Validate(regUser);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().PropertyName == "Email");

        }

        [TestCase("test")]
        [TestCase("test123")]
        [TestCase("test1!")]
        [TestCase("Test12")]
        [TestCase("T!1es")]
        public void VerifyError_OnWrongEmail(string password)
        {
            var regUser = new RegisterUserCommand
            {
                Name = "Test",
                Email = "test@test.com",
                Password = password
            };
            var result = _subejct.Validate(regUser);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().PropertyName == "Password");
        }
    }
}
