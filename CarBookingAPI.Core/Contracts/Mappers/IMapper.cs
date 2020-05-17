using CarBookingAPI.Core.Contracts.Models;
using CarBookingAPI.Core.Contracts.ViewModels;
using System.Collections.Generic;

namespace CarBookingAPI.Core.Contracts.Mappers
{
    /// <summary>
    /// Object for mapping between TModel and TViewModel in both directions.
    /// </summary>
    /// <typeparam name="TViewModel">Valid TViewModel class.</typeparam>
    /// <typeparam name="TModel">Valid TModel class.</typeparam>
    public interface IMapper<TViewModel, TModel>
        where TViewModel : IViewModel
        where TModel : IModel
    {
        /// <summary>
        /// Map TViewModel to TModel.
        /// </summary>
        /// <param name="viewModel">Valid TViewModel.</param>
        /// <returns>Mapped TModel.</returns>
        TModel ToModel(TViewModel viewModel);

        /// <summary>
        /// Map TModel to TViewModel.
        /// </summary>
        /// <param name="model">Valid TModel.</param>
        /// <returns>Mapped TViewModel.</returns>
        TViewModel ToViewModel(TModel model);

        /// <summary>
        /// Map collection of TModel to collection of TViewModel.
        /// </summary>
        /// <param name="modelCollection">Valid TModel collection.</param>
        /// <returns>TViewModel collection.</returns>
        ICollection<TViewModel> ToViewModelCollection(ICollection<TModel> modelCollection);
    }
}
