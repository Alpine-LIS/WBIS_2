using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class NullableDeviceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_amphibian_elements_amphibian_element_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_amphibian_surveys_amphibian_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_botanical_elements_botanical_element_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_site_callings_site_calling_id",
                table: "device_infos");

            migrationBuilder.AlterColumn<Guid>(
                name: "site_calling_id",
                table: "device_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "owl_banding_id",
                table: "device_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_id",
                table: "device_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_element_id",
                table: "device_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_survey_id",
                table: "device_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_element_id",
                table: "device_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_amphibian_elements_amphibian_element_id",
                table: "device_infos",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_amphibian_surveys_amphibian_survey_id",
                table: "device_infos",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_botanical_elements_botanical_element_id",
                table: "device_infos",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_site_callings_site_calling_id",
                table: "device_infos",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_amphibian_elements_amphibian_element_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_amphibian_surveys_amphibian_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_botanical_elements_botanical_element_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_site_callings_site_calling_id",
                table: "device_infos");

            migrationBuilder.AlterColumn<Guid>(
                name: "site_calling_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "owl_banding_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_survey_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_element_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_survey_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_element_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_amphibian_elements_amphibian_element_id",
                table: "device_infos",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_amphibian_surveys_amphibian_survey_id",
                table: "device_infos",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_botanical_elements_botanical_element_id",
                table: "device_infos",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_site_callings_site_calling_id",
                table: "device_infos",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
