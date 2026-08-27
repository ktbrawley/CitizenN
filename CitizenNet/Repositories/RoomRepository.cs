using CitizenNet.Data;
using CitizenNet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.API.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Get(int index)
        {
            return await _context?.Rooms?.Where(r => r.Id == index)?.FirstOrDefaultAsync();
        }

        public async Task<Room> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await _context?.Rooms?.Where(r => r.RoomNumber.ToLower() == name.ToLower())?.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Room>> FindAvailableAsync(int hotelId, DateTime checkIn, DateTime checkOut, int guestCount)
        {
            var unavailableRoomIds = await _context.RoomNights
                .Where(rn => rn.Date >= checkIn && rn.Date < checkOut)
                .Select(rn => rn.RoomId)
                .Distinct()
                .ToListAsync();

            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .Where(r => r.Capacity >= guestCount)
                .Where(r => !unavailableRoomIds.Contains(r.Id))
                .ToListAsync();
        }
    }
}