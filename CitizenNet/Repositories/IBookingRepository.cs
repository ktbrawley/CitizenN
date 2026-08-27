using CitizenNet.Core.Models;
using CitizenNet.Data.Entities;

namespace CitizenNet.API.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> MakeABookingAsync(BookingDto booking);

        Task<Booking?> GetByReferenceAsync(Guid reference);
    }
}