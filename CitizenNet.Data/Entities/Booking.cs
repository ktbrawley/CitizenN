using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.Data.Entities
{
    [Table("Bookings")]
    [PrimaryKey(nameof(Id))]
    [Index(nameof(Reference), IsUnique = true)]
    public class Booking
    {
        public int Id { get; set; }

        public Guid Reference { get; set; } = Guid.NewGuid();

        public int RoomId { get; set; }

        public Room? Room { get; set; }

        public int GuestCount { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}