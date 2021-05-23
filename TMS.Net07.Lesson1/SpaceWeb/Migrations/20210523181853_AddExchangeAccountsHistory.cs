using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddExchangeAccountsHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExchDate",
                table: "ExchangeRatesToUsdHistory",
                newName: "ExchRateDate");

            migrationBuilder.CreateTable(
                name: "ExchangeAccountHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyFrom = table.Column<int>(type: "int", nullable: false),
                    CurrencyTo = table.Column<int>(type: "int", nullable: false),
                    TypeOfExch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeAccountHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeAccountHistory_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeAccountHistory_OwnerId",
                table: "ExchangeAccountHistory",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeAccountHistory");

            migrationBuilder.RenameColumn(
                name: "ExchRateDate",
                table: "ExchangeRatesToUsdHistory",
                newName: "ExchDate");
        }
    }
}
