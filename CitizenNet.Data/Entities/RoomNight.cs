using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.Data.Entities
{
    [Table("RoomNights")]
    [PrimaryKey(nameof(Id))]
    [Index(nameof(RoomId), nameof(Date), IsUnique = true)]
    public class RoomNight
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public Room? Room { get; set; }

        public DateOnly Date { get; set; }

        public int BookingId { get; set; }

        public Booking? Booking { get; set; }
    }
}
