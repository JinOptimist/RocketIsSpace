using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class EditInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InsuranceTypes");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "InsuranceTypes",
                newName: "InsurancePeriod");

            migrationBuilder.RenameColumn(
                name: "CostPerMonth",
                table: "InsuranceTypes",
                newName: "InsuranceNameType");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "InsuranceTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceTypeId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurances_InsuranceTypes_InsuranceTypeId",
                        column: x => x.InsuranceTypeId,
                        principalTable: "InsuranceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurances_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceTypeId",
                table: "Insurances",
                column: "InsuranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_OwnerId",
                table: "Insurances",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "InsuranceTypes");

            migrationBuilder.RenameColumn(
                name: "InsurancePeriod",
                table: "InsuranceTypes",
                newName: "Period");

            migrationBuilder.RenameColumn(
                name: "InsuranceNameType",
                table: "InsuranceTypes",
                newName: "CostPerMonth");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InsuranceTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
