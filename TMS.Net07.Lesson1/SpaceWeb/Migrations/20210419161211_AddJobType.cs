using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Migrations
{
    public partial class AddJobType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: JobType.Junior);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Users");
        }
    }
}
