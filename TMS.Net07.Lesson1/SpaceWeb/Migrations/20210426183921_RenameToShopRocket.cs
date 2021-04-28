using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class RenameToShopRocket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionsExample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopRocket",
                table: "AddShopRocket");

            migrationBuilder.RenameTable(
                name: "AddShopRocket",
                newName: "ShopRocket");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopRocket",
                table: "ShopRocket",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopRocket",
                table: "ShopRocket");

            migrationBuilder.RenameTable(
                name: "ShopRocket",
                newName: "AddShopRocket");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddShopRocket",
                table: "AddShopRocket",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AdditionsExample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BotanicalCenterCount = table.Column<int>(type: "int", nullable: false),
                    ObservarionDeckCount = table.Column<int>(type: "int", nullable: false),
                    RescueCapsuleCount = table.Column<int>(type: "int", nullable: false),
                    RestRoomCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionsExample", x => x.Id);
                });
        }
    }
}
