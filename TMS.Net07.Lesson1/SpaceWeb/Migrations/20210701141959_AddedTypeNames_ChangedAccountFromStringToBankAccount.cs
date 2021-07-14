using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddedTypeNames_ChangedAccountFromStringToBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Payment");

            migrationBuilder.AddColumn<long>(
                name: "BankAccountId",
                table: "Payment",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BankAccountId",
                table: "Payment",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_BankAccount_BankAccountId",
                table: "Payment",
                column: "BankAccountId",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_BankAccount_BankAccountId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BankAccountId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
