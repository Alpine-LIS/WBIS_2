using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ProtectionZoneSpellingFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                table: "hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_protection_zones_preotection_zone_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_protection_zones_preotection_zone_id",
                table: "site_callings");

            migrationBuilder.RenameColumn(
                name: "preotection_zone_id",
                table: "site_callings",
                newName: "protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_preotection_zone_id",
                table: "site_callings",
                newName: "IX_site_callings_protection_zone_id");

            migrationBuilder.RenameColumn(
                name: "preotection_zone_id",
                table: "owl_bandings",
                newName: "protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_preotection_zone_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_protection_zone_id");

            migrationBuilder.RenameColumn(
                name: "current_preotection_zone_id",
                table: "hex160s",
                newName: "current_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_current_preotection_zone_id",
                table: "hex160s",
                newName: "IX_hex160s_current_protection_zone_id");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_current_protection_zone_id",
                table: "hex160s",
                column: "current_protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_protection_zones_protection_zone_id",
                table: "owl_bandings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_protection_zones_protection_zone_id",
                table: "site_callings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_current_protection_zone_id",
                table: "hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_protection_zones_protection_zone_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_protection_zones_protection_zone_id",
                table: "site_callings");

            migrationBuilder.RenameColumn(
                name: "protection_zone_id",
                table: "site_callings",
                newName: "preotection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_protection_zone_id",
                table: "site_callings",
                newName: "IX_site_callings_preotection_zone_id");

            migrationBuilder.RenameColumn(
                name: "protection_zone_id",
                table: "owl_bandings",
                newName: "preotection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_protection_zone_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_preotection_zone_id");

            migrationBuilder.RenameColumn(
                name: "current_protection_zone_id",
                table: "hex160s",
                newName: "current_preotection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_current_protection_zone_id",
                table: "hex160s",
                newName: "IX_hex160s_current_preotection_zone_id");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                table: "hex160s",
                column: "current_preotection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_protection_zones_preotection_zone_id",
                table: "owl_bandings",
                column: "preotection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_protection_zones_preotection_zone_id",
                table: "site_callings",
                column: "preotection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
