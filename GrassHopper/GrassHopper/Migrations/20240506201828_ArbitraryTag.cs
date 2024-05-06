using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrassHopper.Migrations
{
    public partial class ArbitraryTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagText = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhotoGroupGroupId = table.Column<int>(type: "int", nullable: true),
                    PhotoId = table.Column<int>(type: "int", nullable: true),
                    PortfolioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagText);
                    table.ForeignKey(
                        name: "FK_Tag_PhotoGroups_PhotoGroupGroupId",
                        column: x => x.PhotoGroupGroupId,
                        principalTable: "PhotoGroups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_Tag_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId");
                    table.ForeignKey(
                        name: "FK_Tag_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PhotoGroupGroupId",
                table: "Tag",
                column: "PhotoGroupGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PhotoId",
                table: "Tag",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PortfolioId",
                table: "Tag",
                column: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
