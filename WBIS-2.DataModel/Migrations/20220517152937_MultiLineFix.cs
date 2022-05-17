using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class MultiLineFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<MultiLineString>(
                name: "geometry",
                table: "botanical_surveys",
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
                table: "botanical_surveys",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(MultiLineString),
                oldType: "geometry(MultiLineString,26710)",
                oldNullable: true);
        }
    }
}
