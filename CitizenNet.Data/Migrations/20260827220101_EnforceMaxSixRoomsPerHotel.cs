using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizenNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnforceMaxSixRoomsPerHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE TRIGGER trg_Rooms_MaxSixPerHotel
                BEFORE INSERT ON Rooms
                WHEN (SELECT COUNT(*) FROM Rooms WHERE HotelId = NEW.HotelId) >= 6
                BEGIN
                    SELECT RAISE(ABORT, 'A hotel cannot have more than 6 rooms.');
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Rooms_MaxSixPerHotel;");
        }
    }
}
