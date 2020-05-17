using CarBookingAPI.Core.Contracts.ViewModels;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class AuthenticateViewModel : IViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
