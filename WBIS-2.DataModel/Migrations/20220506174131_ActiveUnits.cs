using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ActiveUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_active_botanical_survey_areas_botanical_survey_areas_botani~",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_active_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.RenameColumn(
                name: "hex160_id",
                schema: "public",
                table: "active_hex160s",
                newName: "unit_id");

            migrationBuilder.RenameIndex(
                name: "IX_active_hex160s_hex160_id",
                schema: "public",
                table: "active_hex160s",
                newName: "IX_active_hex160s_unit_id");

            migrationBuilder.RenameColumn(
                name: "botanical_survey_area_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                newName: "unit_id");

            migrationBuilder.RenameIndex(
                name: "IX_active_botanical_survey_areas_botanical_survey_area_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                newName: "IX_active_botanical_survey_areas_unit_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "hex160s",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "botanical_survey_areas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_active_botanical_survey_areas_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "unit_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_active_hex160s_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s",
                column: "unit_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_active_botanical_survey_areas_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_active_hex160s_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "hex160s");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "botanical_survey_areas");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                schema: "public",
                table: "active_hex160s",
                newName: "hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_active_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s",
                newName: "IX_active_hex160s_hex160_id");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                newName: "botanical_survey_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_active_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                newName: "IX_active_botanical_survey_areas_botanical_survey_area_id");

            migrationBuilder.AddForeignKey(
                name: "FK_active_botanical_survey_areas_botanical_survey_areas_botani~",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_active_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "active_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
