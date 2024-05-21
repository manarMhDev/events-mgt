using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rmre1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_TitleSecond_TitleSecondId",
                table: "Invitation");

            migrationBuilder.AlterColumn<int>(
                name: "TitleSecondId",
                table: "Invitation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_TitleSecond_TitleSecondId",
                table: "Invitation",
                column: "TitleSecondId",
                principalSchema: "Lookups",
                principalTable: "TitleSecond",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_TitleSecond_TitleSecondId",
                table: "Invitation");

            migrationBuilder.AlterColumn<int>(
                name: "TitleSecondId",
                table: "Invitation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_TitleSecond_TitleSecondId",
                table: "Invitation",
                column: "TitleSecondId",
                principalSchema: "Lookups",
                principalTable: "TitleSecond",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
