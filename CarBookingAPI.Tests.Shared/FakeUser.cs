using Bogus;
using CarBookingAPI.Infrastructure.ViewModels;

namespace CarBookingAPI.Tests.Shared
{
    public class FakeUser
    {
        public CredentialsViewModel CreateSingle()
        {
            return new Faker<CredentialsViewModel>()
                .RuleFor(x => x.Username, fake => fake.Person.UserName)
                .RuleFor(x => x.Password, fake => fake.Person.Random.AlphaNumeric(10))
                .Generate();
        }
    }
}
