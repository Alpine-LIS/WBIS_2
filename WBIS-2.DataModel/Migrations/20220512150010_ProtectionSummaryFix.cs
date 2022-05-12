using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ProtectionSummaryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plant_protection_summaries_regions_region_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropIndex(
                name: "IX_plant_protection_summaries_region_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "plant_protection_summaries");

            migrationBuilder.AddColumn<Guid>(
                name: "district_id",
                table: "plant_protection_summaries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_plant_protection_summaries_district_id",
                table: "plant_protection_summaries",
                column: "district_id");

            migrationBuilder.AddForeignKey(
                name: "FK_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropIndex(
                name: "IX_plant_protection_summaries_district_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropColumn(
                name: "district_id",
                table: "plant_protection_summaries");

            migrationBuilder.AddColumn<Guid>(
                name: "region_id",
                table: "plant_protection_summaries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_plant_protection_summaries_region_id",
                table: "plant_protection_summaries",
                column: "region_id");

            migrationBuilder.AddForeignKey(
                name: "FK_plant_protection_summaries_regions_region_id",
                table: "plant_protection_summaries",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
