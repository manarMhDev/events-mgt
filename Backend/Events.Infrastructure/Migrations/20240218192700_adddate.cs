using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Invitation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Invitation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Invitation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Invitation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Invitation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_CreatedById",
                table: "Invitation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_LastModifiedById",
                table: "Invitation",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_AspNetUsers_CreatedById",
                table: "Invitation",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_AspNetUsers_LastModifiedById",
                table: "Invitation",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_AspNetUsers_CreatedById",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_AspNetUsers_LastModifiedById",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_CreatedById",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_LastModifiedById",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Invitation");
        }
    }
}
