using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class fun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_Users_UserId",
                table: "BanksCard");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BanksCard_BanksCardId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "FromAccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ToAccountId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "BanksCardId",
                table: "Transaction",
                newName: "BanksCardToId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_BanksCardId",
                table: "Transaction",
                newName: "IX_Transaction_BanksCardToId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BanksCard",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BanksCard_UserId",
                table: "BanksCard",
                newName: "IX_BanksCard_OwnerId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TransferAmount",
                table: "Transaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BanksCardFromId",
                table: "Transaction",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BanksCardFromId",
                table: "Transaction",
                column: "BanksCardFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_Users_OwnerId",
                table: "BanksCard",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BanksCard_BanksCardFromId",
                table: "Transaction",
                column: "BanksCardFromId",
                principalTable: "BanksCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BanksCard_BanksCardToId",
                table: "Transaction",
                column: "BanksCardToId",
                principalTable: "BanksCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanksCard_Users_OwnerId",
                table: "BanksCard");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BanksCard_BanksCardFromId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BanksCard_BanksCardToId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_BanksCardFromId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "BanksCardFromId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "BanksCardToId",
                table: "Transaction",
                newName: "BanksCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_BanksCardToId",
                table: "Transaction",
                newName: "IX_Transaction_BanksCardId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "BanksCard",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BanksCard_OwnerId",
                table: "BanksCard",
                newName: "IX_BanksCard_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "TransferAmount",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "FromAccountId",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToAccountId",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BanksCard_Users_UserId",
                table: "BanksCard",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BanksCard_BanksCardId",
                table: "Transaction",
                column: "BanksCardId",
                principalTable: "BanksCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
