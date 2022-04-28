using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class BotanicalElementNulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_survey_areas_botanical_survey_~",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_area_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_survey_areas_botanical_survey_~",
                table: "botanical_elements",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_survey_areas_botanical_survey_~",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_area_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_survey_areas_botanical_survey_~",
                table: "botanical_elements",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
