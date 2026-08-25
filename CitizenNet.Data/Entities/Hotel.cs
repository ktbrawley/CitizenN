using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.Data.Entities
{
    [Table("Hotels")]
    [PrimaryKey(nameof(Id))]
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Room> Rooms { get; set; } = new();
    }
}
