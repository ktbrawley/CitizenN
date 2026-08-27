using CitizenNet.Data;
using CitizenNet.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CitizenNet.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeedDataController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IHostEnvironment _environment;

        public SeedDataController(HotelDbContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            if (_environment.IsProduction())
            {
                return NotFound();
            }

            await ClearAllAsync();

            var overlook = BuildHotel("The Overlook Hotel");
            var cauldronLake = BuildHotel("Cauldron Lake Lodge");
            var silentHill = BuildHotel("Lakeview Hotel");
            var raccoonCity = BuildHotel("Wrenwood Hotel");
            _context.Hotels.AddRange(overlook, cauldronLake, silentHill, raccoonCity);
            await _context.SaveChangesAsync();

            // Pre-existing bookings so a room-search has something real to exclude.
            await SeedExistingBookingAsync(overlook.Rooms.First(r => r.RoomType == RoomType.Double));
            await SeedExistingBookingAsync(cauldronLake.Rooms.First(r => r.RoomType == RoomType.Deluxe));
            await SeedExistingBookingAsync(silentHill.Rooms.First(r => r.RoomType == RoomType.Single));
            await SeedExistingBookingAsync(raccoonCity.Rooms.Last(r => r.RoomType == RoomType.Double));

            return Ok();
        }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset()
        {
            if (_environment.IsProduction())
            {
                return NotFound();
            }

            await ClearAllAsync();
            return Ok();
        }

        private async Task ClearAllAsync()
        {
            _context.RoomNights.RemoveRange(_context.RoomNights);
            _context.Bookings.RemoveRange(_context.Bookings);
            _context.Rooms.RemoveRange(_context.Rooms);
            _context.Hotels.RemoveRange(_context.Hotels);
            await _context.SaveChangesAsync();
        }

        private async Task SeedExistingBookingAsync(Room room)
        {
            var checkIn = DateTime.Today.AddDays(1);
            var checkOut = checkIn.AddDays(2);

            var booking = new Booking
            {
                RoomId = room.Id,
                GuestCount = room.Capacity,
                CheckIn = checkIn,
                CheckOut = checkOut,
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            for (var date = checkIn; date < checkOut; date = date.AddDays(1))
            {
                _context.RoomNights.Add(new RoomNight { RoomId = room.Id, Date = date, BookingId = booking.Id });
            }
            await _context.SaveChangesAsync();
        }

        private static Hotel BuildHotel(string name)
        {
            var hotel = new Hotel { Name = name };
            hotel.Rooms.Add(new Room { RoomNumber = "101", RoomType = RoomType.Single, Capacity = 1 });
            hotel.Rooms.Add(new Room { RoomNumber = "102", RoomType = RoomType.Single, Capacity = 1 });
            hotel.Rooms.Add(new Room { RoomNumber = "201", RoomType = RoomType.Double, Capacity = 2 });
            hotel.Rooms.Add(new Room { RoomNumber = "202", RoomType = RoomType.Double, Capacity = 2 });
            hotel.Rooms.Add(new Room { RoomNumber = "301", RoomType = RoomType.Deluxe, Capacity = 3 });
            hotel.Rooms.Add(new Room { RoomNumber = "302", RoomType = RoomType.Deluxe, Capacity = 3 });
            return hotel;
        }
    }
}