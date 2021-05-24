using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Migrations
{
    public partial class StateToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: OrderStates.Pending);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");
        }
    }
}
