using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class OneInOneDependecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientForeignKey",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeForeignKey",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientForeignKey",
                table: "Users",
                column: "ClientForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeForeignKey",
                table: "Users",
                column: "EmployeForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientForeignKey",
                table: "Users",
                column: "ClientForeignKey",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employes_EmployeForeignKey",
                table: "Users",
                column: "EmployeForeignKey",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientForeignKey",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employes_EmployeForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClientForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeForeignKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientForeignKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeForeignKey",
                table: "Users");
        }
    }
}
