using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddUserProfileLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserRef",
                table: "UserProfile",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserRef",
                table: "UserProfile",
                column: "UserRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Users_UserRef",
                table: "UserProfile",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Users_UserRef",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_UserRef",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "UserProfile");
        }
    }
}
