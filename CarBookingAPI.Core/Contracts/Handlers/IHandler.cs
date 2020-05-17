using CarBookingAPI.Core.Contracts.ViewModels;
using System;
using System.Threading.Tasks;

namespace CarBookingAPI.Core.Contracts.Handlers
{
    /// <summary>
    /// Handler contract.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public interface IHandler<TViewModel>
        where TViewModel : IViewModel
    {
        /// <summary>
        /// Creates a new record for model equivalent to TViewModel.
        /// </summary>
        /// <param name="viewModel">Non nullable object with all required filled data.</param>
        /// <returns>TViewModel of newly created record with filled id.</returns>
        Task<TViewModel> Add(TViewModel viewModel);

        /// <summary>
        /// Update existing record for model equivalent to TViewModel.
        /// </summary>
        /// <param name="viewModel">Non nullable object with all required filled data.</param>
        /// <returns>TViewModel of updated existing record with filled id.</returns>
        Task<TViewModel> Update(TViewModel viewModel);

        /// <summary>
        /// Delete existing record for model that contains given id.
        /// </summary>
        /// <param name="id">Valid id for a record we want to delete.</param>
        /// <returns>Number of fulfilled database actions.</returns>
        Task<int?> Delete(Guid id);

        /// <summary>
        /// Get single TViewModel equivalent of searched model.
        /// </summary>
        /// <param name="id">Valid id of a model we are searching for.</param>
        /// <returns>Single TViewModel for a given id.</returns>
        Task<TViewModel> GetById(Guid id);
    }
}
