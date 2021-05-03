using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class RenamedTableToOrdersEmployes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLists_Employes_EmployeId",
                table: "OrderLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLists_Orders_OrderId",
                table: "OrderLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLists",
                table: "OrderLists");

            migrationBuilder.RenameTable(
                name: "OrderLists",
                newName: "OrdersEmployes");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLists_OrderId",
                table: "OrdersEmployes",
                newName: "IX_OrdersEmployes_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLists_EmployeId",
                table: "OrdersEmployes",
                newName: "IX_OrdersEmployes_EmployeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersEmployes",
                table: "OrdersEmployes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersEmployes_Employes_EmployeId",
                table: "OrdersEmployes",
                column: "EmployeId",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersEmployes_Orders_OrderId",
                table: "OrdersEmployes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersEmployes_Employes_EmployeId",
                table: "OrdersEmployes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersEmployes_Orders_OrderId",
                table: "OrdersEmployes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersEmployes",
                table: "OrdersEmployes");

            migrationBuilder.RenameTable(
                name: "OrdersEmployes",
                newName: "OrderLists");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersEmployes_OrderId",
                table: "OrderLists",
                newName: "IX_OrderLists_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersEmployes_EmployeId",
                table: "OrderLists",
                newName: "IX_OrderLists_EmployeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLists",
                table: "OrderLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLists_Employes_EmployeId",
                table: "OrderLists",
                column: "EmployeId",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLists_Orders_OrderId",
                table: "OrderLists",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
