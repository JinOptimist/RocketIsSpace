using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class LinkRocketOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderRocket",
                columns: table => new
                {
                    OrderedById = table.Column<long>(type: "bigint", nullable: false),
                    RocketsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRocket", x => new { x.OrderedById, x.RocketsId });
                    table.ForeignKey(
                        name: "FK_OrderRocket_Orders_OrderedById",
                        column: x => x.OrderedById,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRocket_Rockets_RocketsId",
                        column: x => x.RocketsId,
                        principalTable: "Rockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRocket_RocketsId",
                table: "OrderRocket",
                column: "RocketsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRocket");
        }
    }
}
