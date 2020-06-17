using CarBookingAPI.Infrastructure.ViewModels;
using FluentValidation;

namespace CarBookingAPI.Validations
{
    public class CarsValidator : AbstractValidator<CarsViewModel>
    {
        public CarsValidator()
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15);

            RuleFor(x => x.Model)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);

            RuleFor(x => x.Type)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);
        }
    }
}
