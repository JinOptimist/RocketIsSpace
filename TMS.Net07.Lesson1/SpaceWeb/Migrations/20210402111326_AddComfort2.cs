using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddComfort2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SleepingCapsulesCiunt",
                table: "Comforts",
                newName: "SleepingCapsulesCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SleepingCapsulesCount",
                table: "Comforts",
                newName: "SleepingCapsulesCiunt");
        }
    }
}
