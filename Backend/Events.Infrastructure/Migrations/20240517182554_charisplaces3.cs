using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class charisplaces3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventSeatId",
                table: "Invitation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlaceSeats",
                schema: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventPlaceId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceSeats_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlaceSeats_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlaceSeats_ChairTypes_SeatTypeId",
                        column: x => x.SeatTypeId,
                        principalSchema: "Lookups",
                        principalTable: "ChairTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlaceSeats_EventPlaces_EventPlaceId",
                        column: x => x.EventPlaceId,
                        principalSchema: "Event",
                        principalTable: "EventPlaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventSeats",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvitationId = table.Column<int>(type: "int", nullable: false),
                    PlaceSeatId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSeats_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSeats_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSeats_Invitation_InvitationId",
                        column: x => x.InvitationId,
                        principalTable: "Invitation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSeats_PlaceSeats_PlaceSeatId",
                        column: x => x.PlaceSeatId,
                        principalSchema: "Event",
                        principalTable: "PlaceSeats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_CreatedById",
                schema: "Events",
                table: "EventSeats",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_InvitationId",
                schema: "Events",
                table: "EventSeats",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_LastModifiedById",
                schema: "Events",
                table: "EventSeats",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_PlaceSeatId",
                schema: "Events",
                table: "EventSeats",
                column: "PlaceSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSeats_CreatedById",
                schema: "Event",
                table: "PlaceSeats",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSeats_EventPlaceId",
                schema: "Event",
                table: "PlaceSeats",
                column: "EventPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSeats_LastModifiedById",
                schema: "Event",
                table: "PlaceSeats",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceSeats_SeatTypeId",
                schema: "Event",
                table: "PlaceSeats",
                column: "SeatTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventSeats",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "PlaceSeats",
                schema: "Event");

            migrationBuilder.DropColumn(
                name: "EventSeatId",
                table: "Invitation");
        }
    }
}
