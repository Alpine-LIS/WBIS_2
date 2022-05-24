using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class PCS_PZ_Districts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "district_id",
                table: "protection_zones",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "district_id",
                table: "permanent_call_stations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_protection_zones_district_id",
                table: "protection_zones",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_stations_district_id",
                table: "permanent_call_stations",
                column: "district_id");

            migrationBuilder.AddForeignKey(
                name: "FK_permanent_call_stations_districts_district_id",
                table: "permanent_call_stations",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_protection_zones_districts_district_id",
                table: "protection_zones",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_districts_district_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_districts_district_id",
                table: "protection_zones");

            migrationBuilder.DropIndex(
                name: "IX_protection_zones_district_id",
                table: "protection_zones");

            migrationBuilder.DropIndex(
                name: "IX_permanent_call_stations_district_id",
                table: "permanent_call_stations");

            migrationBuilder.DropColumn(
                name: "district_id",
                table: "protection_zones");

            migrationBuilder.DropColumn(
                name: "district_id",
                table: "permanent_call_stations");
        }
    }
}
