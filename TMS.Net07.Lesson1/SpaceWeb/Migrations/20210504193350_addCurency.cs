using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class addCurency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_Users_UserId",
                table: "BanksCard");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BanksCard",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BanksCard_UserId",
                table: "BanksCard",
                newName: "IX_BanksCard_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "BanksCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PinCard",
                table: "BanksCard",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_Users_OwnerId",
                table: "BanksCard",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_Users_OwnerId",
                table: "BanksCard");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BanksCard");

            migrationBuilder.DropColumn(
                name: "PinCard",
                table: "BanksCard");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "BanksCard",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BanksCard_OwnerId",
                table: "BanksCard",
                newName: "IX_BanksCard_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_Users_UserId",
                table: "BanksCard",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
