using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddNavigationProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MyFavouriteRocketId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "Rockets",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QaId",
                table: "Rockets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MyFavouriteRocketId",
                table: "Users",
                column: "MyFavouriteRocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Rockets_AuthorId",
                table: "Rockets",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rockets_QaId",
                table: "Rockets",
                column: "QaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rockets_Users_AuthorId",
                table: "Rockets",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rockets_Users_QaId",
                table: "Rockets",
                column: "QaId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rockets_MyFavouriteRocketId",
                table: "Users",
                column: "MyFavouriteRocketId",
                principalTable: "Rockets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rockets_Users_AuthorId",
                table: "Rockets");

            migrationBuilder.DropForeignKey(
                name: "FK_Rockets_Users_QaId",
                table: "Rockets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rockets_MyFavouriteRocketId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MyFavouriteRocketId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rockets_AuthorId",
                table: "Rockets");

            migrationBuilder.DropIndex(
                name: "IX_Rockets_QaId",
                table: "Rockets");

            migrationBuilder.DropColumn(
                name: "MyFavouriteRocketId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Rockets");

            migrationBuilder.DropColumn(
                name: "QaId",
                table: "Rockets");
        }
    }
}
