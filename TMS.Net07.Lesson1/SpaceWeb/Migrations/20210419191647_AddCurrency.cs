using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.Models;

namespace SpaceWeb.Migrations
{
    public partial class AddCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "BankAccount",
                type: "int",
                nullable: false,
                defaultValue: Currency.BYN);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankAccount");
        }
    }
}
