using CarBookingAPI.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarBookingAPI.Infrastructure.Context
{
    public class CarBookingContext : DbContext
    {
        public CarBookingContext(DbContextOptions<CarBookingContext> options) : base(options)
        {

        }

        public DbSet<Cars> Cars { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reservations> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Users
            modelBuilder.Entity<Users>()
                .Property(x => x.Username)
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Users>()
                .Property(x => x.HashedPassword)
                .IsRequired();

            modelBuilder.Entity<Users>()
                .Property(x => x.Salt)
                .IsRequired();
            #endregion

            #region Cars
            modelBuilder.Entity<Cars>()
                .Property(x => x.Manufacturer)
                .IsRequired();

            modelBuilder.Entity<Cars>()
                .Property(x => x.Model)
                .IsRequired();

            modelBuilder.Entity<Cars>()
                .Property(x => x.Type)
                .IsRequired();
            #endregion

            #region Reservations
            modelBuilder.Entity<Reservations>()
                .Property(x => x.CarId)
                .IsRequired();

            modelBuilder.Entity<Reservations>()
                .Property(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<Reservations>()
                .Property(x => x.ReservedAt)
                .IsRequired();

            modelBuilder.Entity<Reservations>()
                .Property(x => x.ReservedUntil)
                .IsRequired();
            #endregion
        }
    }
}
