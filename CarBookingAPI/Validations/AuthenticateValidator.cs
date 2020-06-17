using CarBookingAPI.Infrastructure.ViewModels;
using FluentValidation;

namespace CarBookingAPI.Validations
{
    public class AuthenticateValidator : AbstractValidator<CredentialsViewModel>
    {
        public AuthenticateValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(40);
        }
    }
}
