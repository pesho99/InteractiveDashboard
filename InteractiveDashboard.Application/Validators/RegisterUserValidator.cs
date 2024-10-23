using FluentValidation;
using InteractiveDashboard.Application.Handlers.Register;

namespace InteractiveDashboard.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
               .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
               .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
               .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
               .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]").WithMessage("Password must contain at least one special character.");
        }
    }
}
