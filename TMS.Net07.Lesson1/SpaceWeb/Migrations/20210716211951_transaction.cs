using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.CreateTable(
                name: "TransactionBank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionNumber = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BanksCardFromId = table.Column<long>(type: "bigint", nullable: true),
                    BanksCardToId = table.Column<long>(type: "bigint", nullable: true),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionBank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionBank_BanksCard_BanksCardFromId",
                        column: x => x.BanksCardFromId,
                        principalTable: "BanksCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionBank_BanksCard_BanksCardToId",
                        column: x => x.BanksCardToId,
                        principalTable: "BanksCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBank_BanksCardFromId",
                table: "TransactionBank",
                column: "BanksCardFromId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBank_BanksCardToId",
                table: "TransactionBank",
                column: "BanksCardToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionBank");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanksCardFromId = table.Column<long>(type: "bigint", nullable: true),
                    BanksCardToId = table.Column<long>(type: "bigint", nullable: true),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_BanksCard_BanksCardFromId",
                        column: x => x.BanksCardFromId,
                        principalTable: "BanksCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_BanksCard_BanksCardToId",
                        column: x => x.BanksCardToId,
                        principalTable: "BanksCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BanksCardFromId",
                table: "Transaction",
                column: "BanksCardFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BanksCardToId",
                table: "Transaction",
                column: "BanksCardToId");
        }
    }
}
