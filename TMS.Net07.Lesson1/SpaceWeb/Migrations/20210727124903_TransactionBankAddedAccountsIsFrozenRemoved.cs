using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class TransactionBankAddedAccountsIsFrozenRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReceiverAccountId",
                table: "TransactionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SenderAccountId",
                table: "TransactionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBank_ReceiverAccountId",
                table: "TransactionBank",
                column: "ReceiverAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBank_SenderAccountId",
                table: "TransactionBank",
                column: "SenderAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBank_BankAccount_ReceiverAccountId",
                table: "TransactionBank",
                column: "ReceiverAccountId",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBank_BankAccount_SenderAccountId",
                table: "TransactionBank",
                column: "SenderAccountId",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBank_BankAccount_ReceiverAccountId",
                table: "TransactionBank");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBank_BankAccount_SenderAccountId",
                table: "TransactionBank");

            migrationBuilder.DropIndex(
                name: "IX_TransactionBank_ReceiverAccountId",
                table: "TransactionBank");

            migrationBuilder.DropIndex(
                name: "IX_TransactionBank_SenderAccountId",
                table: "TransactionBank");

            migrationBuilder.DropColumn(
                name: "ReceiverAccountId",
                table: "TransactionBank");

            migrationBuilder.DropColumn(
                name: "SenderAccountId",
                table: "TransactionBank");
        }
    }
}
