using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SiteCallingKeysUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_site_callings_guid",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_user_locations_site_calling_detections_site_calling_detecti~",
                table: "user_locations");

            migrationBuilder.DropIndex(
                name: "IX_user_locations_site_calling_detection_id",
                table: "user_locations");

            migrationBuilder.DropColumn(
                name: "site_calling_detection_id",
                table: "user_locations");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_site_calling_id",
                table: "site_calling_detections",
                column: "site_calling_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_user_location_id",
                table: "site_calling_detections",
                column: "user_location_id");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_site_callings_site_calling_id",
                table: "site_calling_detections",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_user_locations_user_location_id",
                table: "site_calling_detections",
                column: "user_location_id",
                principalTable: "user_locations",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_site_callings_site_calling_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_user_locations_user_location_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_site_calling_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_user_location_id",
                table: "site_calling_detections");

            migrationBuilder.AddColumn<Guid>(
                name: "site_calling_detection_id",
                table: "user_locations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_site_calling_detection_id",
                table: "user_locations",
                column: "site_calling_detection_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_site_callings_guid",
                table: "site_calling_detections",
                column: "guid",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_locations_site_calling_detections_site_calling_detecti~",
                table: "user_locations",
                column: "site_calling_detection_id",
                principalTable: "site_calling_detections",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
