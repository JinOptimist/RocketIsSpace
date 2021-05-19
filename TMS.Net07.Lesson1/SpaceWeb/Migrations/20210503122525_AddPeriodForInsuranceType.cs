using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddPeriodForInsuranceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostPerYear",
                table: "InsuranceTypes",
                newName: "Period");

            migrationBuilder.AddColumn<int>(
                name: "CostPerMonth",
                table: "InsuranceTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPerMonth",
                table: "InsuranceTypes");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "InsuranceTypes",
                newName: "CostPerYear");
        }
    }
}
