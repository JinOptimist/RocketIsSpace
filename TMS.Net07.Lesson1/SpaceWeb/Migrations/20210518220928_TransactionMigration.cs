using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class TransactionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToAccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BanksCardId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_BanksCard_BanksCardId",
                        column: x => x.BanksCardId,
                        principalTable: "BanksCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BanksCardId",
                table: "Transaction",
                column: "BanksCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
