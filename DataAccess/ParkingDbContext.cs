using Devi.ParkingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Devi.ParkingService.DataAccess
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options)
            : base(options)
        { }


        public DbSet<Location> Locations { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Location>().HasKey(x => x.Id);
            builder.Entity<Location>().HasMany(x => x.Areas);

            builder.Entity<Area>().HasKey(x => x.Id);
            builder.Entity<Area>().HasIndex(x => x.LocationId);
            builder.Entity<Area>().HasOne(x => x.Location)
                .WithMany(x => x.Areas)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Area>().HasMany(x => x.Bookings);

            builder.Entity<Customer>().HasKey(x => x.Id);
            builder.Entity<Customer>().HasMany(x => x.Bookings);
            
            builder.Entity<Booking>().HasKey(x => x.Id);
            builder.Entity<Booking>().HasIndex(x => x.AreaId);
            builder.Entity<Booking>().HasOne(x => x.Area)
                .WithMany(x => x.Bookings)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Booking>().HasOne(x => x.Customer)
                .WithMany(x => x.Bookings)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
