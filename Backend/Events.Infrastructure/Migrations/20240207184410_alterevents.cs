using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Event",
                table: "Events",
                newName: "NameEnglish");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Event",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                schema: "Event",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EventPlaceId",
                schema: "Event",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                schema: "Event",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    TitleFirstId = table.Column<int>(type: "int", nullable: false),
                    TitleSecondId = table.Column<int>(type: "int", nullable: false),
                    PersonTypeId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Whatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Party = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendWhatsapp = table.Column<bool>(type: "bit", nullable: false),
                    SendEmail = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    InvitationStatus = table.Column<int>(type: "int", nullable: false),
                    FormType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "Event",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_PersonTypes_PersonTypeId",
                        column: x => x.PersonTypeId,
                        principalSchema: "Lookups",
                        principalTable: "PersonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_TitleFirst_TitleFirstId",
                        column: x => x.TitleFirstId,
                        principalSchema: "Lookups",
                        principalTable: "TitleFirst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_TitleSecond_TitleSecondId",
                        column: x => x.TitleSecondId,
                        principalSchema: "Lookups",
                        principalTable: "TitleSecond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventPlaceId",
                schema: "Event",
                table: "Events",
                column: "EventPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_EventId",
                table: "Invitation",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_PersonTypeId",
                table: "Invitation",
                column: "PersonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_TitleFirstId",
                table: "Invitation",
                column: "TitleFirstId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_TitleSecondId",
                table: "Invitation",
                column: "TitleSecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventPlaces_EventPlaceId",
                schema: "Event",
                table: "Events",
                column: "EventPlaceId",
                principalSchema: "Event",
                principalTable: "EventPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventPlaces_EventPlaceId",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventPlaceId",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventDate",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventPlaceId",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                schema: "Event",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                schema: "Event",
                table: "Events",
                newName: "Name");
        }
    }
}
