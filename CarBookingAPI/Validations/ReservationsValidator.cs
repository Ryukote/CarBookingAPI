using CarBookingAPI.Infrastructure.ViewModels;
using FluentValidation;

namespace CarBookingAPI.Validations
{
    public class ReservationsValidator : AbstractValidator<ReservationsViewModel>
    {
        public ReservationsValidator()
        {
            RuleFor(x => x.ReservedAt)
                .NotEmpty();

            RuleFor(x => x.ReservedUntil)
                .NotEmpty();

            RuleFor(x => x.CarId)
                .NotNull()
                .SetValidator(new GuidValidator());

            RuleFor(x => x.UserId)
                .NotNull()
                .SetValidator(new GuidValidator());
        }
    }
}
