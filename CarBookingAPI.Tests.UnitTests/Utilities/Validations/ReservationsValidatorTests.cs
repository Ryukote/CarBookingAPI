using CarBookingAPI.Tests.Shared;
using CarBookingAPI.Validations;
using Xunit;

namespace CarBookingAPI.Tests.UnitTests.Utilities.Validations
{
    public class ReservationsValidatorTests
    {
        private ReservationsValidator _validator;
        public ReservationsValidatorTests()
        {
            _validator = new ReservationsValidator();
        }

        [Fact]
        public void WillHaveValidReservation()
        {
            var reservation = new FakeReservation().CreateSingle();

            var validation = _validator.Validate(reservation);

            Assert.True(validation.IsValid);
        }

        [Fact]
        public void WillHaveInvalidReservedAt()
        {
            var reservation = new FakeReservation().CreateSingle();
            reservation.ReservedAt = null;

            var validation = _validator.Validate(reservation);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void WillHaveInvalidReservedUntil()
        {
            var reservation = new FakeReservation().CreateSingle();
            reservation.ReservedUntil = null;

            var validation = _validator.Validate(reservation);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void WillHaveInvalidCarId()
        {
            var reservation = new FakeReservation().CreateSingle();
            reservation.CarId = null;

            var validation = _validator.Validate(reservation);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void WillHaveInvalidUserId()
        {
            var reservation = new FakeReservation().CreateSingle();
            reservation.UserId = null;

            var validation = _validator.Validate(reservation);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }
    }
}
