using CarBookingAPI.Core.Contracts.Handlers;
using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Core.Contracts.ViewModels;
using CarBookingAPI.Infrastructure.Context;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookingAPI.Infrastructure.Handlers
{
    public class ReservationsHandler : IReservationsHandler<ReservationsViewModel>
    {
        private CarBookingContext _context;
        private IMapper<ReservationsViewModel, Reservations> _mapper;
        public ReservationsHandler(CarBookingContext context, IMapper<ReservationsViewModel, Reservations> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationsViewModel> Add(ReservationsViewModel viewModel)
        {
            var reservation = _mapper.ToModel(viewModel);
            reservation.Id = Guid.NewGuid();

            _context.Reservations.Add(reservation);

            if(await _context.SaveChangesAsync() > 0)
            {
                return _mapper.ToViewModel(reservation);
            }

            return null;
        }

        public async Task<int?> Delete(Guid id)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(x => x.Id == id);

            if(reservation != null)
            {
                _context.Remove(reservation);
                return await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<ReservationsViewModel> GetById(Guid id)
        {
            var reservation = await _context.Reservations
                .AsNoTracking()
                .Include(x => x.Car)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return (reservation != null)
                ? _mapper.ToViewModel(reservation)
                : null;
        }

        public async Task<ICollection<ReservationsViewModel>> GetPaginated(IPaginationViewModel<ReservationsViewModel> paginationData)
        {
            var context = _context.Reservations.AsQueryable();

            if(paginationData.QueryData.CarId.HasValue)
            {
                context = context.Where(x => x.CarId == paginationData.QueryData.CarId);
            }

            if(paginationData.QueryData.ReservedAt.HasValue && paginationData.QueryData.ReservedUntil.HasValue)
            {
                context = context.Where(x => x.ReservedAt >= paginationData.QueryData.ReservedUntil
                    && x.ReservedUntil <= paginationData.QueryData.ReservedAt);
            }

            else
            {
                if(paginationData.QueryData.UserId.HasValue)
                {
                    context = context.Where(x => x.UserId == paginationData.QueryData.UserId);
                }

                if(paginationData.QueryData.ReservedUntil.HasValue)
                {
                    context = context.Where(x => x.ReservedUntil == paginationData.QueryData.ReservedUntil);
                }
            }

            var data = await context.ToListAsync();

            return data != null
                ? _mapper.ToViewModelCollection(data)
                : null;
        }

        public async Task<ReservationsViewModel> Update(ReservationsViewModel viewModel)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if(reservation != null)
            {
                reservation.ReservedAt = viewModel.ReservedAt.Value;
                reservation.ReservedUntil = viewModel.ReservedUntil.Value;
                reservation.UserId = viewModel.UserId.Value;
                reservation.CarId = viewModel.CarId.Value;

                _context.Reservations.Update(reservation);

                return await _context.SaveChangesAsync() > 0
                    ? _mapper.ToViewModel(reservation)
                    : null;
            }

            return null;
        }
    }
}
