using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class DetectionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "moused",
                table: "site_callings");

            migrationBuilder.AddColumn<bool>(
                name: "moused",
                table: "site_calling_detections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "moused",
                table: "site_calling_detections");

            migrationBuilder.AddColumn<bool>(
                name: "moused",
                table: "site_callings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
