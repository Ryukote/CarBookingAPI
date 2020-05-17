using CarBookingAPI.Core.Contracts.Models;
using System;

namespace CarBookingAPI.Infrastructure.Models
{
    public class Cars : IModel
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
    }
}
