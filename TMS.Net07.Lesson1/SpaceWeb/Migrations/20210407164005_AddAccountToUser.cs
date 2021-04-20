using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddAccountToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "BankAccount",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_OwnerId",
                table: "BankAccount",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Users_OwnerId",
                table: "BankAccount",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Users_OwnerId",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_OwnerId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BankAccount");
        }
    }
}
