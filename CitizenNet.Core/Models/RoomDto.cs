namespace CitizenNet.Core.Models
{
    /// <summary>
    /// A room returned from an availability search.
    /// </summary>
    public class RoomDto
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public string RoomType { get; set; } = string.Empty;

        public int Capacity { get; set; }
    }
}
