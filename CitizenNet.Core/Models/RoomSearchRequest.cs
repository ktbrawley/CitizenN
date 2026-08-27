using System.ComponentModel.DataAnnotations;

namespace CitizenNet.Core.Models
{
    /// <summary>
    /// Search criteria for finding available rooms at a named hotel for a given stay.
    /// </summary>
    public class RoomSearchRequest
    {
        /// <summary>
        /// The exact name of the hotel to search within.
        /// </summary>
        [Required]
        public string HotelName { get; set; } = string.Empty;

        /// <summary>
        /// The first night of the stay (inclusive).
        /// </summary>
        [Required]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// The day the guest checks out (exclusive: the last booked night is the day before this).
        /// </summary>
        [Required]
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// Number of guests the room must be able to accommodate.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "GuestCount must be at least 1.")]
        public int GuestCount { get; set; } = 1;
    }
}
