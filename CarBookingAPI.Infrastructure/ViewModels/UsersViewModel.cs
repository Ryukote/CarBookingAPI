using CarBookingAPI.Core.Contracts.ViewModels;
using System;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class UsersViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
