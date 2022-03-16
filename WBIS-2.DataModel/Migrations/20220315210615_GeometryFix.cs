using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class GeometryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datum",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "geometry",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "geometry",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropColumn(
                name: "geometry",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "amphibian_locations_found");

            migrationBuilder.DropColumn(
                name: "geometry",
                table: "amphibian_locations_found");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "amphibian_locations_found");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "amphibian_locations_found");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "botanical_elements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "geometry",
                table: "botanical_elements",
                type: "geometry(Point,26710)",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "botanical_elements",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lon",
                table: "botanical_elements",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "amphibian_elements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "geometry",
                table: "amphibian_elements",
                type: "geometry(Point,26710)",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "amphibian_elements",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lon",
                table: "amphibian_elements",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datum",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "geometry",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "amphibian_elements");

            migrationBuilder.DropColumn(
                name: "geometry",
                table: "amphibian_elements");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "amphibian_elements");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "amphibian_elements");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "botanical_points_of_interest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "geometry",
                table: "botanical_points_of_interest",
                type: "geometry(Point,26710)",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "botanical_points_of_interest",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lon",
                table: "botanical_points_of_interest",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "botanical_plants_of_interest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "geometry",
                table: "botanical_plants_of_interest",
                type: "geometry(Point,26710)",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "botanical_plants_of_interest",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lon",
                table: "botanical_plants_of_interest",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "amphibian_points_of_interest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "geometry",
                table: "amphibian_points_of_interest",
                type: "geometry(Point,26710)",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "amphibian_points_of_interest",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lon",
                table: "amphibian_points_of_interest",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "amphibian_locations_found",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "geometry",
                table: "amphibian_locations_found",
                type: "geometry(Point,26710)",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "amphibian_locations_found",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lon",
                table: "amphibian_locations_found",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
