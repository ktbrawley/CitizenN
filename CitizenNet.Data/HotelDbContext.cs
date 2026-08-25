using CitizenNet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<RoomNight> RoomNights => Set<RoomNight>();
    }
}
