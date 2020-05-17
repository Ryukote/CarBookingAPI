using CarBookingAPI.Core.Contracts.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarBookingAPI.Core.Contracts.Handlers
{
    /// <summary>
    /// Object for paginating data.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public interface IPaginated<TViewModel>
        where TViewModel : IViewModel
    {
        /// <summary>
        /// Get paginated data.
        /// </summary>
        /// <param name="paginationData">Valid IPagionationViewModel.</param>
        /// <returns>Paginated data.</returns>
        Task<ICollection<TViewModel>> GetPaginated(IPaginationViewModel<TViewModel> paginationData);
    }
}
