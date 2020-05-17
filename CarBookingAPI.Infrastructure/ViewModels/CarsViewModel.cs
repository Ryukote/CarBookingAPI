using CarBookingAPI.Core.Contracts.ViewModels;
using System;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class CarsViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
    }
}
