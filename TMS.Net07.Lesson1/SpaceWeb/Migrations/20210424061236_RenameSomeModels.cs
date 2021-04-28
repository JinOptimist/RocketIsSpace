using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class RenameSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionsOrder");

            migrationBuilder.DropTable(
                name: "ComfortsOrder");

            migrationBuilder.DropColumn(
                name: "KitchenSeatsCount",
                table: "Comforts");

            migrationBuilder.DropColumn(
                name: "SleepingCapsulesCount",
                table: "Comforts");

            migrationBuilder.DropColumn(
                name: "StorageCapacity",
                table: "Comforts");

            migrationBuilder.DropColumn(
                name: "ToiletCount",
                table: "Comforts");

            migrationBuilder.DropColumn(
                name: "BotanicalCenterCount",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "ObservarionDeckCount",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "RescueCapsuleCount",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "RestRoomCount",
                table: "Additions");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Comforts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Comforts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Additions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Additions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdditionsExample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestRoomCount = table.Column<int>(type: "int", nullable: false),
                    RescueCapsuleCount = table.Column<int>(type: "int", nullable: false),
                    ObservarionDeckCount = table.Column<int>(type: "int", nullable: false),
                    BotanicalCenterCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionsExample", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComfortsExample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToiletCount = table.Column<int>(type: "int", nullable: false),
                    KitchenSeatsCount = table.Column<int>(type: "int", nullable: false),
                    StorageCapacity = table.Column<int>(type: "int", nullable: false),
                    SleepingCapsulesCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfortsExample", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comforts_OrderId",
                table: "Comforts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Additions_OrderId",
                table: "Additions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Additions_Orders_OrderId",
                table: "Additions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comforts_Orders_OrderId",
                table: "Comforts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additions_Orders_OrderId",
                table: "Additions");

            migrationBuilder.DropForeignKey(
                name: "FK_Comforts_Orders_OrderId",
                table: "Comforts");

            migrationBuilder.DropTable(
                name: "AdditionsExample");

            migrationBuilder.DropTable(
                name: "ComfortsExample");

            migrationBuilder.DropIndex(
                name: "IX_Comforts_OrderId",
                table: "Comforts");

            migrationBuilder.DropIndex(
                name: "IX_Additions_OrderId",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Comforts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Comforts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Additions");

            migrationBuilder.AddColumn<int>(
                name: "KitchenSeatsCount",
                table: "Comforts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SleepingCapsulesCount",
                table: "Comforts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StorageCapacity",
                table: "Comforts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToiletCount",
                table: "Comforts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BotanicalCenterCount",
                table: "Additions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObservarionDeckCount",
                table: "Additions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RescueCapsuleCount",
                table: "Additions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestRoomCount",
                table: "Additions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdditionsOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionsOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionsOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComfortsOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfortsOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComfortsOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionsOrder_OrderId",
                table: "AdditionsOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ComfortsOrder_OrderId",
                table: "ComfortsOrder",
                column: "OrderId");
        }
    }
}
