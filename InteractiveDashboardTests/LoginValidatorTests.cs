using InteractiveDashboard.Application.Handlers.Register;
using InteractiveDashboard.Application.Validators;

namespace InteractiveDashboardTests
{
    public class LoginValidatorTests
    {

        LoginValidator _subejct = new LoginValidator();

        [Test]
        public void VerifyNoError_IfCorrect()
        {
            var regUser = new LoginCommand
            {
                Email = "test@test.com",
                Password = "Passw0rd!1"
            };
            var result = _subejct.Validate(regUser);
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void VerifyError_OnWrongEmail()
        {
            var regUser = new LoginCommand
            {
                Email = "test.com",
                Password = "Passw0rd!1"
            };
            var result = _subejct.Validate(regUser);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.First().PropertyName == "Email");

        }

    }
}
