using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SiteCallingDetectionConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "district_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "hex160_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "quad75_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "watershed_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_district_id",
                table: "site_calling_detections",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_hex160_id",
                table: "site_calling_detections",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_quad75_id",
                table: "site_calling_detections",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_watershed_id",
                table: "site_calling_detections",
                column: "watershed_id");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_districts_district_id",
                table: "site_calling_detections",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_hex160s_hex160_id",
                table: "site_calling_detections",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_quad75s_quad75_id",
                table: "site_calling_detections",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_watersheds_watershed_id",
                table: "site_calling_detections",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_districts_district_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_hex160s_hex160_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_quad75s_quad75_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_watersheds_watershed_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_district_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_hex160_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_quad75_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_watershed_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "district_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "hex160_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "quad75_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "watershed_id",
                table: "site_calling_detections");
        }
    }
}
