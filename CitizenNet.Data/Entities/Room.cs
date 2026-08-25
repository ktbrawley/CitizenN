using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.Data.Entities
{
    [Table("Rooms")]
    [PrimaryKey(nameof(Id))]
    public class Room
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public RoomType RoomType { get; set; }

        public int Capacity { get; set; }
    }
}
