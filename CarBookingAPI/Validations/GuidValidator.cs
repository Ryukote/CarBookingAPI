using FluentValidation;
using System;

namespace CarBookingAPI.Validations
{
    public class GuidValidator : AbstractValidator<Guid?>
    {
        public GuidValidator()
        {
            RuleFor(x => x.HasValue)
                .Equals(true);

            RuleFor(x => x.Value)
                .NotEmpty();
        }
    }
}
