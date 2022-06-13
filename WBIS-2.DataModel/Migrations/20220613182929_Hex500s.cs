using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class Hex500s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "hex500_id",
                table: "site_callings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "hex500_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "hex500s",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex500_id = table.Column<string>(type: "text", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex500s", x => x.guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_hex500_id",
                table: "site_callings",
                column: "hex500_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_hex500_id",
                table: "site_calling_detections",
                column: "hex500_id");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_hex500s_hex500_id",
                table: "site_calling_detections",
                column: "hex500_id",
                principalTable: "hex500s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_hex500s_hex500_id",
                table: "site_callings",
                column: "hex500_id",
                principalTable: "hex500s",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_hex500s_hex500_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_hex500s_hex500_id",
                table: "site_callings");

            migrationBuilder.DropTable(
                name: "hex500s");

            migrationBuilder.DropIndex(
                name: "IX_site_callings_hex500_id",
                table: "site_callings");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_hex500_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "hex500_id",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "hex500_id",
                table: "site_calling_detections");
        }
    }
}
