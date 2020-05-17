using CarBookingAPI.Core.Contracts.Models;
using System;

namespace CarBookingAPI.Infrastructure.Models
{
    public class Reservations : IModel
    {
        public Guid Id { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime ReservedUntil { get; set; }
        public Guid CarId { get; set; }
        public Guid UserId { get; set; }

        public virtual Cars Car { get; set; }
        public virtual Users User { get; set; }
    }
}
