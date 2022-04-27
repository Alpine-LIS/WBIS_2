using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class NullableBotanicalGeometries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "user_locations",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "spi_plant_polygons",
                type: "geometry(Polygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(Polygon,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "spi_plant_points",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_callings",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_detections",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "owl_bandings",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "device_infos",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "botanical_surveys",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)");

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "botanical_survey_areas",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "botanical_elements",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "amphibian_surveys",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "amphibian_elements",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "user_locations",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "spi_plant_polygons",
                type: "geometry(Polygon,26710)",
                nullable: false,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(Polygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "spi_plant_points",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_callings",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_detections",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "owl_bandings",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "device_infos",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "botanical_surveys",
                type: "geometry(LineString,26710)",
                nullable: false,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "botanical_survey_areas",
                type: "geometry(MultiPolygon,26710)",
                nullable: false,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "botanical_elements",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "amphibian_surveys",
                type: "geometry(LineString,26710)",
                nullable: false,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "amphibian_elements",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);
        }
    }
}
