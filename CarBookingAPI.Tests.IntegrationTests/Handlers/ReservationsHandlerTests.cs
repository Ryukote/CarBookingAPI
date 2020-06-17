using CarBookingAPI.Core.Contracts.Mappers;
using CarBookingAPI.Infrastructure.Handlers;
using CarBookingAPI.Infrastructure.Models;
using CarBookingAPI.Infrastructure.Utilities.Mappers;
using CarBookingAPI.Infrastructure.ViewModels;
using CarBookingAPI.Tests.Shared;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarBookingAPI.Tests.IntegrationTests.Handlers
{
    public class ReservationsHandlerTests
    {
        IOptions<AppSettings> _options;
        private IMapper<ReservationsViewModel, Reservations> _mapper;
        public ReservationsHandlerTests()
        {
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(x => x.Value).Returns(new AppSettings());
            mock.Object.Value.HashSecret = "ajsdhajksd7523aasd";
            _options = mock.Object;

            _mapper = new ReservationsMapper();
        }

        [Fact]
        public async Task WillAddReservation()
        {
            var reservation = new FakeReservation().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var reservationHandler = new ReservationsHandler(context, _mapper);

            var result = await reservationHandler.Add(reservation);

            Assert.NotEqual(default, result.Id);
            Assert.NotEqual(default, result.ReservedAt);
            Assert.NotEqual(default, result.ReservedUntil);
        }

        [Fact]
        public async Task WillGetReservation()
        {
            var reservation = new FakeReservation().CreateSingle();

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var reservationHandler = new ReservationsHandler(context, _mapper);

            var result = await reservationHandler.Add(reservation);

            var selected = await reservationHandler.GetById(result.Id.Value);

            Assert.Equal(selected.Id.Value, result.Id.Value);
        }

        [Fact]
        public async Task WillNotGetReservation()
        {
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var reservationHandler = new ReservationsHandler(context, _mapper);

            var selected = await reservationHandler.GetById(default);

            Assert.Null(selected);
        }

        [Fact]
        public async Task WillUpdateReservation()
        {
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var reservationHandler = new ReservationsHandler(context, _mapper);

            var reservation = new FakeReservation().CreateSingle();

            var created = await reservationHandler.Add(reservation);

            created.ReservedUntil = created.ReservedUntil.Value.AddDays(7);

            var updated = await reservationHandler.Update(created);

            Assert.NotEqual(updated.ReservedUntil.Value, reservation.ReservedUntil.Value);
        }

        [Fact]
        public async Task WillDeleteReservation()
        {
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var reservationHandler = new ReservationsHandler(context, _mapper);

            var reservation = new FakeReservation().CreateSingle();

            var created = await reservationHandler.Add(reservation);

            var deleted = await reservationHandler.Delete(created.Id.Value);

            Assert.True(deleted > 0);
        }
    }
}
