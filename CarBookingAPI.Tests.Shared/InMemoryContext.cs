using CarBookingAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CarBookingAPI.Tests.Shared
{
    public class InMemoryContext
    {
        public CarBookingContext Create(string databaseName)
        {
            DbContextOptions<CarBookingContext> options;

            var builder = new DbContextOptionsBuilder<CarBookingContext>();
            builder.UseInMemoryDatabase(databaseName: databaseName);
            options = builder.Options;

            CarBookingContext context = new CarBookingContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
