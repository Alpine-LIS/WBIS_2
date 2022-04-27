using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class NullableBotanyConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "thp_area_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "thp_area_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                table: "botanical_survey_areas",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "thp_area_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "thp_area_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                table: "botanical_survey_areas",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
