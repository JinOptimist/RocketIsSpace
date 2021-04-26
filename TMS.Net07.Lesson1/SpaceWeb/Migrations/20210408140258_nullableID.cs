using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class nullableID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientForeignKey",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employes_EmployeForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClientForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeForeignKey",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "EmployeForeignKey",
                table: "Users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClientForeignKey",
                table: "Users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientForeignKey",
                table: "Users",
                column: "ClientForeignKey",
                unique: true,
                filter: "[ClientForeignKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeForeignKey",
                table: "Users",
                column: "EmployeForeignKey",
                unique: true,
                filter: "[EmployeForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientForeignKey",
                table: "Users",
                column: "ClientForeignKey",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employes_EmployeForeignKey",
                table: "Users",
                column: "EmployeForeignKey",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientForeignKey",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employes_EmployeForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClientForeignKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeForeignKey",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "EmployeForeignKey",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClientForeignKey",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientForeignKey",
                table: "Users",
                column: "ClientForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeForeignKey",
                table: "Users",
                column: "EmployeForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientForeignKey",
                table: "Users",
                column: "ClientForeignKey",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employes_EmployeForeignKey",
                table: "Users",
                column: "EmployeForeignKey",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
