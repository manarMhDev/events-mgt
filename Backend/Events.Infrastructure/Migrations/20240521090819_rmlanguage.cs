using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rmlanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Lookups",
                table: "TitleSecond");

            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Lookups",
                table: "TitleFirst");

            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Lookups",
                table: "PersonTypes");

            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Event",
                table: "EventPlaces");

            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Lookups",
                table: "ChairTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "Lookups",
                table: "TitleSecond",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "Lookups",
                table: "TitleFirst",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "Lookups",
                table: "PersonTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "Event",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "Event",
                table: "EventPlaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "Lookups",
                table: "ChairTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
