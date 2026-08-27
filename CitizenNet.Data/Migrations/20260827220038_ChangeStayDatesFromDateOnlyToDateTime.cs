using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizenNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStayDatesFromDateOnlyToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // No DDL: Booking.CheckIn/CheckOut and RoomNight.Date changed from DateOnly to
            // DateTime, but both map to SQLite's TEXT storage class, so there's no column to
            // alter. This migration exists to record the model-snapshot change on its own,
            // separate from the room-limit trigger.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
