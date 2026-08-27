using CitizenNet.Core.Models;
using CitizenNet.Data;
using CitizenNet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _context;

        public BookingRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> MakeABookingAsync(BookingDto booking)
        {
            if (booking.CheckOut <= booking.CheckIn)
            {
                throw new ArgumentException("'CheckOut' must be after 'CheckIn'.", nameof(booking));
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == booking.RoomId);
            if (room is null)
            {
                throw new KeyNotFoundException($"Room {booking.RoomId} was not found.");
            }

            if (booking.GuestCount > room.Capacity)
            {
                throw new ArgumentException("Guest count exceeds the room's capacity.", nameof(booking));
            }

            var nights = new List<DateTime>();
            for (var date = booking.CheckIn; date < booking.CheckOut; date = date.AddDays(1))
            {
                nights.Add(date);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = new Booking
                {
                    RoomId = booking.RoomId,
                    GuestCount = booking.GuestCount,
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                };
                _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();

                foreach (var night in nights)
                {
                    _context.RoomNights.Add(new RoomNight { RoomId = booking.RoomId, Date = night, BookingId = entity.Id });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("The room is not available for the requested dates.", ex);
            }
        }

        public async Task<Booking?> GetByReferenceAsync(Guid reference)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Reference == reference);
        }
    }
}