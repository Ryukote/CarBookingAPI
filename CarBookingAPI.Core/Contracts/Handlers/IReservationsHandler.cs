using CarBookingAPI.Core.Contracts.ViewModels;

namespace CarBookingAPI.Core.Contracts.Handlers
{
    public interface IReservationsHandler<TViewModel> : IHandler<TViewModel>, IPaginated<TViewModel>
        where TViewModel : IViewModel
    {
    }
}
