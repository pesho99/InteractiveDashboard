using FluentValidation;
using InteractiveDashboard.Application.Handlers.Register;

namespace InteractiveDashboard.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
