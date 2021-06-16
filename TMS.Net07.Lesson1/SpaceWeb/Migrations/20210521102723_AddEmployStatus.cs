using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.Models.Human;

namespace SpaceWeb.Migrations
{
    public partial class AddEmployStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeStatus",
                table: "Employes",
                type: "int",
                nullable: false,
                defaultValue: EmployeStatus.Request);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeStatus",
                table: "Employes");
        }
    }
}
