using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class DIstrictParentFIx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_districts_district_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "spi_plant_polygons",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_districts_district_id",
                table: "botanical_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_districts_district_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "spi_plant_polygons",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_districts_district_id",
                table: "botanical_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
