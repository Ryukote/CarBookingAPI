using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace CarBookingAPI.Infrastructure.Utilities.Mappers
{
    public class CarsMapper : IMapper<CarsViewModel, Cars>
    {
        public Cars ToModel(CarsViewModel viewModel)
        {
            return new Cars()
            {
                Id = viewModel.Id,
                Manufacturer = viewModel.Manufacturer,
                Model = viewModel.Model,
                Type = viewModel.Type
            };
        }

        public CarsViewModel ToViewModel(Cars model)
        {
            return new CarsViewModel()
            {
                Id = model.Id,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                Type = model.Type
            };
        }

        public ICollection<CarsViewModel> ToViewModelCollection(ICollection<Cars> modelCollection)
        {
            var carCollection = new List<CarsViewModel>();

            foreach(var item in modelCollection)
            {
                carCollection.Add(new CarsViewModel()
                {
                    Id = item.Id,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    Type = item.Type
                });
            }

            return carCollection;
        }
    }
}
