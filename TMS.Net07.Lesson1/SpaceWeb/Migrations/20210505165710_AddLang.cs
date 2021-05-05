using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Migrations
{
    public partial class AddLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lang",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: Lang.Ru);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lang",
                table: "Users");
        }
    }
}
