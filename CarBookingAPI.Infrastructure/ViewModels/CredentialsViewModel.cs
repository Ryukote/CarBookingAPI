using CarBookingAPI.Core.Contracts.ViewModels;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class CredentialsViewModel : IViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
