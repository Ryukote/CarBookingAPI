using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace CarBookingAPI.Infrastructure.Utilities.Mappers
{
    public class ReservationsMapper : IMapper<ReservationsViewModel, Reservations>
    {
        public Reservations ToModel(ReservationsViewModel viewModel)
        {
            return new Reservations()
            {
                CarId = viewModel.CarId.HasValue ? viewModel.CarId.Value : default,
                ReservedAt = viewModel.ReservedAt.Value,
                ReservedUntil = viewModel.ReservedUntil.Value,
                UserId = viewModel.UserId.HasValue ? viewModel.UserId.Value : default,
                Car = viewModel.Car != null ? new CarsMapper().ToModel(viewModel.Car) : null,
                User = viewModel.User != null ? new UsersMapper().ToModel(viewModel.User) : null
            };
        }

        public ReservationsViewModel ToViewModel(Reservations model)
        {
            var carsMapper = new CarsMapper();
            var usersMapper = new UsersMapper();

            return new ReservationsViewModel()
            {
                Car = carsMapper.ToViewModel(model.Car),
                CarId = model.CarId,
                ReservedAt = model.ReservedAt,
                ReservedUntil = model.ReservedUntil,
                User = usersMapper.ToViewModel(model.User),
                UserId = model.UserId,
                Id = model.Id
            };
        }

        public ICollection<ReservationsViewModel> ToViewModelCollection(ICollection<Reservations> modelCollection)
        {
            throw new System.NotImplementedException();
        }
    }
}
