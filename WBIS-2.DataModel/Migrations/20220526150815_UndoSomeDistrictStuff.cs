using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UndoSomeDistrictStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_districts_district_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_districts_district_id",
                table: "protection_zones");

            migrationBuilder.DropTable(
                name: "hex160_required_passes_districts",
                schema: "public");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "hex160_required_passes_districts",
                schema: "public",
                columns: table => new
                {
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_required_pass_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160_required_passes_districts", x => new { x.district_id, x.hex160_required_pass_id });
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_districts_hex160_required_passes_hex~",
                        column: x => x.hex160_required_pass_id,
                        principalTable: "hex160_required_passes",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_protection_zones_district_id",
                table: "protection_zones",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_stations_district_id",
                table: "permanent_call_stations",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160_required_passes_districts_hex160_required_pass_id",
                schema: "public",
                table: "hex160_required_passes_districts",
                column: "hex160_required_pass_id");

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
    }
}
