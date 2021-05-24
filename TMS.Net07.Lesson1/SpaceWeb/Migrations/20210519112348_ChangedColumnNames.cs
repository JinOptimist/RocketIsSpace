using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class ChangedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "Employes",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "DepartmentType",
                table: "Departments",
                newName: "DepartmentSpecificationType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Employes",
                newName: "Specification");

            migrationBuilder.RenameColumn(
                name: "DepartmentSpecificationType",
                table: "Departments",
                newName: "DepartmentType");
        }
    }
}
