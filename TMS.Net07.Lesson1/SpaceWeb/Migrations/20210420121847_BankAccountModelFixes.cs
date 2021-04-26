using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class BankAccountModelFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BankAccountId",
                table: "BankAccount",
                newName: "AccountNumber");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BankAccount",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "BankAccount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "BankAccount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "BankAccount",
                newName: "BankAccountId");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "BankAccount",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
