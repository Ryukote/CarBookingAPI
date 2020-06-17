using CarBookingAPI.Tests.Shared;
using CarBookingAPI.Validations;
using Xunit;

namespace CarBookingAPI.Tests.UnitTests.Utilities.Validations
{
    public class CarsValidatorTests
    {
        private CarsValidator _validator;
        public CarsValidatorTests()
        {
            _validator = new CarsValidator();
        }

        [Fact]
        public void WillHaveValidCar()
        {
            var car = new FakeCar().CreateSingle();

            var validation = _validator.Validate(car);

            Assert.True(validation.IsValid);
        }

        [Fact]
        public void WillHaveInvalidManufacturer()
        {
            var car = new FakeCar().CreateSingle();
            car.Manufacturer = "";

            var validation = _validator.Validate(car);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void WillHaveInvalidModel()
        {
            var car = new FakeCar().CreateSingle();
            car.Model = "";

            var validation = _validator.Validate(car);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void WillHaveInvalidType()
        {
            var car = new FakeCar().CreateSingle();
            car.Type = "";

            var validation = _validator.Validate(car);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }
    }
}
