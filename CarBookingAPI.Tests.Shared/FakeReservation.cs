using CarBookingAPI.Infrastructure.ViewModels;
using System;

namespace CarBookingAPI.Tests.Shared
{
    public class FakeReservation
    {
        public ReservationsViewModel CreateSingle()
        {
            var userId = Guid.NewGuid();
            var carId = Guid.NewGuid();
            var user = new FakeUser().CreateSingle();

            var car = new FakeCar().CreateSingle();

            return new ReservationsViewModel()
            {
                CarId = carId,
                Car = car,
                User = new UsersViewModel()
                {
                    Id = userId,
                    Username = user.Username
                },
                UserId = userId,
                ReservedAt = DateTime.UtcNow,
                ReservedUntil = DateTime.UtcNow.AddDays(1)
            };
        }
    }
}
