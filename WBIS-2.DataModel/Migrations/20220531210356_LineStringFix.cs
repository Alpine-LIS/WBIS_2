using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class LineStringFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "site_calling_tracks",
                type: "geometry(MultiLineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "amphibian_surveys",
                type: "geometry(MultiLineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "site_calling_tracks",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(MultiLineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "amphibian_surveys",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(MultiLineString,26710)",
                oldNullable: true);
        }
    }
}
