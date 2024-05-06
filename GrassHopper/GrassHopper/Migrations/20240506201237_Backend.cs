using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrassHopper.Migrations
{
    public partial class Backend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "PhotoGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PortfolioName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PortfolioSummary = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PortfolioDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PortfolioThumbnailPhotoId = table.Column<int>(type: "int", nullable: true),
                    IsHidden = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.PortfolioId);
                    table.ForeignKey(
                        name: "FK_Portfolios_Photos_PortfolioThumbnailPhotoId",
                        column: x => x.PortfolioThumbnailPhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGroups_PortfolioId",
                table: "PhotoGroups",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_PortfolioThumbnailPhotoId",
                table: "Portfolios",
                column: "PortfolioThumbnailPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoGroups_Portfolios_PortfolioId",
                table: "PhotoGroups",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoGroups_Portfolios_PortfolioId",
                table: "PhotoGroups");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_PhotoGroups_PortfolioId",
                table: "PhotoGroups");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "PhotoGroups");
        }
    }
}
