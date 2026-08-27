namespace CitizenNet.Core.Models
{
    public class RoomSearchRequest
    {
        public string HotelName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestCount { get; set; } = 0;
    }
}