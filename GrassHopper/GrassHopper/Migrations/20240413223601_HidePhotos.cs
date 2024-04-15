using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrassHopper.Migrations
{
    public partial class HidePhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Photos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "PhotoGroups",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "PhotoGroups");
        }
    }
}
