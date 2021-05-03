using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceWeb.Migrations
{
    public partial class AddHumanProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumanProfiles");
        }
    }
}
