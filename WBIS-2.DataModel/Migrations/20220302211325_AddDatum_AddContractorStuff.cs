using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AddDatum_AddContractorStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "watersheds",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "user_locations",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "user_locations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_callings",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "site_callings",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "site_calling_tracks",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "site_calling_repository_tracks",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_repository_detections",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "site_calling_repository_detections",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_repositories",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "site_calling_repositories",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_detections",
                type: "geometry(Point,26710)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "site_calling_detections",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Polygon>(
                name: "geometry",
                table: "quad75s",
                type: "geometry(Polygon,26710)",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "protection_zones",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "permanent_call_stations",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Polygon>(
                name: "geometry",
                table: "hex160s",
                type: "geometry(Polygon,26710)",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "districts",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "district_extended_geometry",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "device_infos",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "datum",
                table: "device_infos",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Polygon>(
                name: "poly_geometry",
                table: "deleted_geometries",
                type: "geometry(Polygon,26710)",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "point_eometry",
                table: "deleted_geometries",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "mpoly_geometry",
                table: "deleted_geometries",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "line_geometry",
                table: "deleted_geometries",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "cnddb_occurrences",
                type: "geometry(MultiPolygon,26710)",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "cdfw_spotted_owls",
                type: "geometry(Point,26710)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "cdfw_spotted_owl_diagrams",
                type: "geometry(LineString,26710)",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "modified_user_id",
                table: "application_users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_application_users_modified_user_id",
                table: "application_users",
                column: "modified_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_users_application_users_modified_user_id",
                table: "application_users",
                column: "modified_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_users_application_users_modified_user_id",
                table: "application_users");

            migrationBuilder.DropIndex(
                name: "IX_application_users_modified_user_id",
                table: "application_users");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "user_locations");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "site_calling_repository_detections");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "site_calling_repositories");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "modified_user_id",
                table: "application_users");

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "watersheds",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "user_locations",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_callings",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "site_calling_tracks",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "site_calling_repository_tracks",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_repository_detections",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_repositories",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "site_calling_detections",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)");

            migrationBuilder.AlterColumn<Polygon>(
                name: "geometry",
                table: "quad75s",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "protection_zones",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "permanent_call_stations",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Polygon>(
                name: "geometry",
                table: "hex160s",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "districts",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "district_extended_geometry",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "device_infos",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Polygon>(
                name: "poly_geometry",
                table: "deleted_geometries",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "point_eometry",
                table: "deleted_geometries",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "mpoly_geometry",
                table: "deleted_geometries",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "line_geometry",
                table: "deleted_geometries",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "geometry",
                table: "cnddb_occurrences",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "geometry",
                table: "cdfw_spotted_owls",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,26710)",
                oldNullable: true);

            migrationBuilder.AlterColumn<LineString>(
                name: "geometry",
                table: "cdfw_spotted_owl_diagrams",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(LineString),
                oldType: "geometry(LineString,26710)",
                oldNullable: true);
        }
    }
}
