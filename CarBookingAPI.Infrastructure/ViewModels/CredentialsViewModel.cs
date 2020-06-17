using CarBookingAPI.Core.Contracts.ViewModels;
using System;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class CredentialsViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
