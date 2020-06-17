using CarBookingAPI.Tests.Shared;
using CarBookingAPI.Validations;
using Xunit;

namespace CarBookingAPI.Tests.UnitTests.Utilities.Validations
{
    public class AuthenticateValidatorTests
    {
        private AuthenticateValidator _validator;
        public AuthenticateValidatorTests()
        {
            _validator = new AuthenticateValidator();
        }

        [Fact]
        public void WillHaveValidUser()
        {
            var user = new FakeUser().CreateSingle();

            var validation = _validator.Validate(user);

            Assert.True(validation.IsValid);
        }

        [Fact]
        public void WillHaveInvalidUsername()
        {
            var user = new FakeUser().CreateSingle();
            user.Username = "";

            var validation = _validator.Validate(user);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void WillHaveInvalidPassword()
        {
            var user = new FakeUser().CreateSingle();
            user.Password = "";

            var validation = _validator.Validate(user);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }
    }
}
