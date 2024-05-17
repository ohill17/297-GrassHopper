using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrassHopper.Migrations
{
    public partial class PortfolioFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoGroups_Portfolios_PortfolioId",
                table: "PhotoGroups");

            migrationBuilder.DropIndex(
                name: "IX_PhotoGroups_PortfolioId",
                table: "PhotoGroups");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "PhotoGroups");

            migrationBuilder.CreateTable(
                name: "PhotoGroupPortfolio",
                columns: table => new
                {
                    AssocPortfoliosPortfolioId = table.Column<int>(type: "int", nullable: false),
                    PortfolioPGroupsGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGroupPortfolio", x => new { x.AssocPortfoliosPortfolioId, x.PortfolioPGroupsGroupId });
                    table.ForeignKey(
                        name: "FK_PhotoGroupPortfolio_PhotoGroups_PortfolioPGroupsGroupId",
                        column: x => x.PortfolioPGroupsGroupId,
                        principalTable: "PhotoGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoGroupPortfolio_Portfolios_AssocPortfoliosPortfolioId",
                        column: x => x.AssocPortfoliosPortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGroupPortfolio_PortfolioPGroupsGroupId",
                table: "PhotoGroupPortfolio",
                column: "PortfolioPGroupsGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoGroupPortfolio");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "PhotoGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGroups_PortfolioId",
                table: "PhotoGroups",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoGroups_Portfolios_PortfolioId",
                table: "PhotoGroups",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId");
        }
    }
}
