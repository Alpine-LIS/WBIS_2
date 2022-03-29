using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class HexPzFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                table: "hex160s");

            migrationBuilder.AlterColumn<Guid>(
                name: "current_preotection_zone_id",
                table: "hex160s",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                table: "hex160s",
                column: "current_preotection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                table: "hex160s");

            migrationBuilder.AlterColumn<Guid>(
                name: "current_preotection_zone_id",
                table: "hex160s",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                table: "hex160s",
                column: "current_preotection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
