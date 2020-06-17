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
    public class CarsHandler : IHandler<CarsViewModel>, IPaginated<CarsViewModel>
    {
        private CarBookingContext _context;
        private IMapper<CarsViewModel, Cars> _carMapper;
        public CarsHandler(CarBookingContext context, IMapper<CarsViewModel, Cars> carMapper)
        {
            _context = context;
            _carMapper = carMapper;
        }

        public async Task<CarsViewModel> Add(CarsViewModel viewModel)
        {
            var car = _carMapper.ToModel(viewModel);
            car.Id = Guid.NewGuid();

            _context.Cars.Add(car);

            if (await _context.SaveChangesAsync() == 1)
            {
                return _carMapper.ToViewModel(car);
            }

            return null;
        }

        public async Task<int?> Delete(Guid id)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            if(car != null)
            {
                _context.Remove(car);
                return await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<CarsViewModel> GetById(Guid id)
        {
            var car = await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return (car != null) ? _carMapper.ToViewModel(car) : null;
        }

        public async Task<ICollection<CarsViewModel>> GetPaginated(IPaginationViewModel<CarsViewModel> paginationData)
        {
            var queryResult = _context.Cars.AsQueryable();

            if(!string.IsNullOrEmpty(paginationData.QueryData.Manufacturer))
            {
                queryResult = queryResult.Where(x => x.Manufacturer.Contains(paginationData.QueryData.Manufacturer));
            }

            if(!string.IsNullOrEmpty(paginationData.QueryData.Model))
            {
                queryResult = queryResult.Where(x => x.Model.Contains(paginationData.QueryData.Model));
            }

            if(!string.IsNullOrEmpty(paginationData.QueryData.Type))
            {
                queryResult = queryResult.Where(x => x.Type.Contains(paginationData.QueryData.Type));
            }

            var result = await queryResult
                .Skip(paginationData.Skip)
                .Take(paginationData.Take)
                .ToListAsync();

            return _carMapper.ToViewModelCollection(result);
        }

        public async Task<CarsViewModel> Update(CarsViewModel viewModel)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if(car != null)
            {
                car.Id = viewModel.Id;
                car.Model = viewModel.Model;
                car.Manufacturer = viewModel.Manufacturer;
                car.Type = viewModel.Type;

                _context.Cars.Update(car);

                return await _context.SaveChangesAsync() > 0 ? _carMapper.ToViewModel(car) : null;
            }

            return null;
        }
    }
}
