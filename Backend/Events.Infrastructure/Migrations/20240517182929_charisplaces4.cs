using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class charisplaces4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EventSeats",
                schema: "Events",
                newName: "EventSeats",
                newSchema: "Event");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Events");

            migrationBuilder.RenameTable(
                name: "EventSeats",
                schema: "Event",
                newName: "EventSeats",
                newSchema: "Events");
        }
    }
}
