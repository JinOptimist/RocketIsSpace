using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class ChandegDependenciesRenamedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_HumanProfiles_ForeignKeyProfile",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Employes_HumanProfiles_ForeignKeyProfile",
                table: "Employes");

            migrationBuilder.DropTable(
                name: "HumanProfiles");

            migrationBuilder.RenameColumn(
                name: "ForeignKeyProfile",
                table: "Employes",
                newName: "ForeignKeyUser");

            migrationBuilder.RenameIndex(
                name: "IX_Employes_ForeignKeyProfile",
                table: "Employes",
                newName: "IX_Employes_ForeignKeyUser");

            migrationBuilder.RenameColumn(
                name: "ForeignKeyProfile",
                table: "Clients",
                newName: "ForeignKeyUser");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_ForeignKeyProfile",
                table: "Clients",
                newName: "IX_Clients_ForeignKeyUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_ForeignKeyUser",
                table: "Clients",
                column: "ForeignKeyUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employes_Users_ForeignKeyUser",
                table: "Employes",
                column: "ForeignKeyUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_ForeignKeyUser",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Employes_Users_ForeignKeyUser",
                table: "Employes");

            migrationBuilder.RenameColumn(
                name: "ForeignKeyUser",
                table: "Employes",
                newName: "ForeignKeyProfile");

            migrationBuilder.RenameIndex(
                name: "IX_Employes_ForeignKeyUser",
                table: "Employes",
                newName: "IX_Employes_ForeignKeyProfile");

            migrationBuilder.RenameColumn(
                name: "ForeignKeyUser",
                table: "Clients",
                newName: "ForeignKeyProfile");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_ForeignKeyUser",
                table: "Clients",
                newName: "IX_Clients_ForeignKeyProfile");

            migrationBuilder.CreateTable(
                name: "HumanProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForeignKeyUser = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumanProfiles_Users_ForeignKeyUser",
                        column: x => x.ForeignKeyUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HumanProfiles_ForeignKeyUser",
                table: "HumanProfiles",
                column: "ForeignKeyUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_HumanProfiles_ForeignKeyProfile",
                table: "Clients",
                column: "ForeignKeyProfile",
                principalTable: "HumanProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employes_HumanProfiles_ForeignKeyProfile",
                table: "Employes",
                column: "ForeignKeyProfile",
                principalTable: "HumanProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
