using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.Models;

namespace SpaceWeb.Migrations
{
    public partial class AddDefaultCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultCurrency",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: Currency.BYN);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCurrency",
                table: "Users");
        }
    }
}
