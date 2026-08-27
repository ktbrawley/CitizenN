namespace CitizenNet.Core.Models
{
    public class BookingDto
    {
        public Guid Reference { get; set; }

        public int RoomId { get; set; }

        public int GuestCount { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}