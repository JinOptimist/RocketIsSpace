using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class updateBankCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_Users_OwnerId",
                table: "BanksCard");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "BanksCard",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BanksCard_OwnerId",
                table: "BanksCard",
                newName: "IX_BanksCard_UserId");

            migrationBuilder.AddColumn<long>(
                name: "BankAccountId",
                table: "BanksCard",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BanksCard_BankAccountId",
                table: "BanksCard",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_BankAccount_BankAccountId",
                table: "BanksCard",
                column: "BankAccountId",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_Users_UserId",
                table: "BanksCard",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_BankAccount_BankAccountId",
                table: "BanksCard");

            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_Users_UserId",
                table: "BanksCard");

            migrationBuilder.DropIndex(
                name: "IX_BanksCard_BankAccountId",
                table: "BanksCard");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "BanksCard");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BanksCard",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BanksCard_UserId",
                table: "BanksCard",
                newName: "IX_BanksCard_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_Users_OwnerId",
                table: "BanksCard",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
