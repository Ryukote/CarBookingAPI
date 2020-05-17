using CarBookingAPI.Core.Contracts.ViewModels;

namespace CarBookingAPI.Infrastructure.ViewModels
{
    public class QueryViewModel<TViewModel> : IPaginationViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public TViewModel QueryData { get; set; }
    }
}
