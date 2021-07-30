using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class BankAccountHistoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankAccountHistoryId",
                table: "TransactionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankAccountHistoryId1",
                table: "TransactionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankAccountHistoryId",
                table: "Payment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankAccountHistoryId",
                table: "BanksCard",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccountHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountType = table.Column<int>(type: "int", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserWhoChangedId = table.Column<long>(type: "bigint", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountHistory_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccountHistory_Users_UserWhoChangedId",
                        column: x => x.UserWhoChangedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBank_BankAccountHistoryId",
                table: "TransactionBank",
                column: "BankAccountHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBank_BankAccountHistoryId1",
                table: "TransactionBank",
                column: "BankAccountHistoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BankAccountHistoryId",
                table: "Payment",
                column: "BankAccountHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BanksCard_BankAccountHistoryId",
                table: "BanksCard",
                column: "BankAccountHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountHistory_OwnerId",
                table: "BankAccountHistory",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountHistory_UserWhoChangedId",
                table: "BankAccountHistory",
                column: "UserWhoChangedId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_BankAccountHistory_BankAccountHistoryId",
                table: "BanksCard",
                column: "BankAccountHistoryId",
                principalTable: "BankAccountHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_BankAccountHistory_BankAccountHistoryId",
                table: "Payment",
                column: "BankAccountHistoryId",
                principalTable: "BankAccountHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBank_BankAccountHistory_BankAccountHistoryId",
                table: "TransactionBank",
                column: "BankAccountHistoryId",
                principalTable: "BankAccountHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBank_BankAccountHistory_BankAccountHistoryId1",
                table: "TransactionBank",
                column: "BankAccountHistoryId1",
                principalTable: "BankAccountHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_BankAccountHistory_BankAccountHistoryId",
                table: "BanksCard");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_BankAccountHistory_BankAccountHistoryId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBank_BankAccountHistory_BankAccountHistoryId",
                table: "TransactionBank");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBank_BankAccountHistory_BankAccountHistoryId1",
                table: "TransactionBank");

            migrationBuilder.DropTable(
                name: "BankAccountHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionBank_BankAccountHistoryId",
                table: "TransactionBank");

            migrationBuilder.DropIndex(
                name: "IX_TransactionBank_BankAccountHistoryId1",
                table: "TransactionBank");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BankAccountHistoryId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_BanksCard_BankAccountHistoryId",
                table: "BanksCard");

            migrationBuilder.DropColumn(
                name: "BankAccountHistoryId",
                table: "TransactionBank");

            migrationBuilder.DropColumn(
                name: "BankAccountHistoryId1",
                table: "TransactionBank");

            migrationBuilder.DropColumn(
                name: "BankAccountHistoryId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "BankAccountHistoryId",
                table: "BanksCard");
        }
    }
}
