using CarBookingAPI.Core.Contracts.ViewModels;
using System;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class ReservationsViewModel : IViewModel
    {
        public Guid? Id { get; set; }
        public DateTime? ReservedAt { get; set; }
        public DateTime? ReservedUntil { get; set; }
        public Guid? CarId { get; set; }
        public Guid? UserId { get; set; }

        public CarsViewModel Car { get; set; }
        public UsersViewModel User { get; set; }
    }
}
