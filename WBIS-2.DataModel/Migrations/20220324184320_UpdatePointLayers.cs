using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UpdatePointLayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_lon",
                table: "user_locations",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "user_lat",
                table: "user_locations",
                newName: "lat");

            migrationBuilder.RenameColumn(
                name: "starting_lon",
                table: "site_callings",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "starting_lat",
                table: "site_callings",
                newName: "lat");

            migrationBuilder.RenameColumn(
                name: "detection_lon",
                table: "site_calling_detections",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "detection_lat",
                table: "site_calling_detections",
                newName: "lat");

            migrationBuilder.RenameColumn(
                name: "device_lon",
                table: "device_infos",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "device_lat",
                table: "device_infos",
                newName: "lat");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "device_infos",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lon",
                table: "user_locations",
                newName: "user_lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "user_locations",
                newName: "user_lat");

            migrationBuilder.RenameColumn(
                name: "lon",
                table: "site_callings",
                newName: "starting_lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "site_callings",
                newName: "starting_lat");

            migrationBuilder.RenameColumn(
                name: "lon",
                table: "site_calling_detections",
                newName: "detection_lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "site_calling_detections",
                newName: "detection_lat");

            migrationBuilder.RenameColumn(
                name: "lon",
                table: "device_infos",
                newName: "device_lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "device_infos",
                newName: "device_lat");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "device_infos",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");
        }
    }
}
