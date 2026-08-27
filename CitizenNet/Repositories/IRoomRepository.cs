using CitizenNet.Data.Entities;

namespace CitizenNet.API.Repositories
{
    public interface IRoomRepository : IReadOnlyRepository<Room>
    {
        Task<IReadOnlyList<Room>> FindAvailableAsync(int hotelId, DateTime checkIn, DateTime checkOut, int guestCount);
    }
}