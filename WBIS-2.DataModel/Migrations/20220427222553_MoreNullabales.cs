using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class MoreNullabales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                table: "botanical_surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_area_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                table: "botanical_surveys",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                table: "botanical_surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_area_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                table: "botanical_surveys",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
