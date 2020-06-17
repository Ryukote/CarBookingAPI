using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Handlers;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.Utilities.Mappers;
using CarBookingAPI.Infrastructure.ViewModels;
using CarBookingAPI.Tests.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarBookingAPI.Tests.IntegrationTests.Handlers
{
    public class CarsHandlerTests
    {
        private IMapper<CarsViewModel, Cars> _mapper;

        public CarsHandlerTests()
        {
            _mapper = new CarsMapper();
        }

        [Fact]
        public async Task WillAddCar()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            Assert.NotEqual(default, result.Id);
            Assert.NotEqual(default, result.Manufacturer);
            Assert.NotEqual(default, result.Model);
            Assert.NotEqual(default, result.Type);
        }

        [Fact]
        public async Task WillGetCar()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            var selected = await handler.GetById(result.Id);

            Assert.NotEqual(default, selected.Id);
            Assert.NotEqual(default, selected.Manufacturer);
            Assert.NotEqual(default, selected.Model);
            Assert.NotEqual(default, selected.Type);
        }

        [Fact]
        public async Task WillNotGetCar()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            var selected = await handler.GetById(default);

            Assert.Null(selected);
        }

        [Fact]
        public async Task WillUpdateCarManufacturer()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            CarsViewModel updatedCar = new CarsViewModel();
            updatedCar.Id = result.Id;
            updatedCar.Manufacturer = "Daihatsu";
            updatedCar.Model = result.Model;
            updatedCar.Type = result.Type;

            var updated = await handler.Update(updatedCar);

            Assert.Equal(result.Id, updated.Id);
            Assert.NotEqual(result.Manufacturer, updated.Manufacturer);
            Assert.Equal(result.Model, updated.Model);
            Assert.Equal(result.Type, updated.Type);
        }

        [Fact]
        public async Task WillUpdateCarModel()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            CarsViewModel updatedCar = new CarsViewModel();
            updatedCar.Id = result.Id;
            updatedCar.Manufacturer = result.Manufacturer;
            updatedCar.Model = "Sahabatku";
            updatedCar.Type = result.Type;

            var updated = await handler.Update(updatedCar);

            Assert.Equal(result.Id, updated.Id);
            Assert.Equal(result.Manufacturer, updatedCar.Manufacturer);
            Assert.NotEqual(result.Model, updatedCar.Model);
            Assert.Equal(result.Type, updatedCar.Type);
        }

        [Fact]
        public async Task WillUpdateCarType()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            CarsViewModel updatedCar = new CarsViewModel();
            updatedCar.Id = result.Id;
            updatedCar.Manufacturer = result.Manufacturer;
            updatedCar.Model = result.Model;
            updatedCar.Type = "Small Type Vehicles";

            var updated = await handler.Update(updatedCar);

            Assert.Equal(result.Id, updated.Id);
            Assert.Equal(result.Manufacturer, updatedCar.Manufacturer);
            Assert.Equal(result.Model, updatedCar.Model);
            Assert.NotEqual(result.Type, updatedCar.Type);
        }

        [Fact]
        public async Task WillDeleteCar()
        {
            var car = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var result = await handler.Add(car);

            var deleted = await handler.Delete(result.Id);

            Assert.True(deleted > 0);
        }

        [Fact]
        public async Task WillGetSpecificCars()
        {
            var firstCar = new FakeCar().CreateSingle();
            firstCar.Manufacturer = "Daihatsu";

            var secondCar = new FakeCar().CreateSingle();
            secondCar.Manufacturer = "Daihatsu";

            var thirdCar = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var firstAddedCar = await handler.Add(firstCar);
            var secondAddedCar = await handler.Add(secondCar);
            var thirdAddedCar = await handler.Add(thirdCar);

            var carSearch = new QueryViewModel<CarsViewModel>()
            {
                QueryData = new CarsViewModel()
                {
                    Manufacturer = "Daih"
                },
                Skip = 0,
                Take = 10
            };

            var selectedDaihatsu = await handler.GetPaginated(carSearch);

            Assert.NotNull(selectedDaihatsu);
            Assert.Equal(2, selectedDaihatsu.Count);
        }

        [Fact]
        public async Task WillNotGetSpecificCars()
        {
            var firstCar = new FakeCar().CreateSingle();
            firstCar.Manufacturer = "Daihatsu";

            var secondCar = new FakeCar().CreateSingle();
            secondCar.Manufacturer = "Daihatsu";

            var thirdCar = new FakeCar().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var handler = new CarsHandler(context, _mapper);
            var firstAddedCar = await handler.Add(firstCar);
            var secondAddedCar = await handler.Add(secondCar);
            var thirdAddedCar = await handler.Add(thirdCar);

            var carSearch = new QueryViewModel<CarsViewModel>()
            {
                QueryData = new CarsViewModel()
                {
                    Manufacturer = "yugo"
                },
                Skip = 0,
                Take = 10
            };

            var selectedDaihatsu = await handler.GetPaginated(carSearch);

            Assert.Empty(selectedDaihatsu);
        }
    }
}
