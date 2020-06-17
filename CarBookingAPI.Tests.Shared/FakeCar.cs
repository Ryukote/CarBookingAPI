using Bogus;
using CarBookingAPI.Infrastructure.ViewModels;

namespace CarBookingAPI.Tests.Shared
{
    public class FakeCar
    {
        public CarsViewModel CreateSingle()
        {
            return new Faker<CarsViewModel>()
                .RuleFor(property => property.Model, fake => fake.Vehicle.Model())
                .RuleFor(property => property.Manufacturer, fake => fake.Vehicle.Manufacturer())
                .RuleFor(property => property.Type, fake => fake.Vehicle.Type())
                .Generate();
        }
    }
}
