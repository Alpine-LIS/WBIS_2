using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class Hex160PzFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_hex160s_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_protection_zones_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_hex160s_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "protection_zone_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_protection_zones_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "hex160_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
