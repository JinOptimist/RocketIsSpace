using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class UserHasMazes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "MazeLevels",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MazeLevels_UserId",
                table: "MazeLevels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MazeLevels_Users_UserId",
                table: "MazeLevels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MazeLevels_Users_UserId",
                table: "MazeLevels");

            migrationBuilder.DropIndex(
                name: "IX_MazeLevels_UserId",
                table: "MazeLevels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MazeLevels");
        }
    }
}
