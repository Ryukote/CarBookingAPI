using CarBookingAPI.Core.Contracts.ViewModels;
using System;
using System.Threading.Tasks;

namespace CarBookingAPI.Core.Contracts.Handlers
{
    public interface IUsersHandler<TViewModel, TCredentials> : IPaginated<TViewModel>
        where TViewModel : IViewModel
        where TCredentials : IViewModel
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="credentials">Valid TCredentials.</param>
        /// <returns>TViewModel of a new user.</returns>
        Task<TViewModel> Add(TCredentials credentials);

        /// <summary>
        /// Update existing user.
        /// </summary>
        /// <param name="credentials">Valid TCredentials.</param>
        /// <returns>TViewModel of updated user.</returns>
        Task<TViewModel> Update(TCredentials credentials);

        /// <summary>
        /// Delete existing user.
        /// </summary>
        /// <param name="id">Valid user id.</param>
        /// <returns>Number of fulfilled database actions.</returns>
        Task<int?> Delete(Guid id);

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">Valid user id.</param>
        /// <returns>TViewModel of selected user.</returns>
        Task<TViewModel> GetById(Guid id);
    }
}
