using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UserRecordsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_modified_id",
                table: "site_callings");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "site_callings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "site_callings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "protection_zones",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "protection_zones",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "permanent_call_stations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "permanent_call_stations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "hex160_required_passes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "hex160_required_passes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_scopings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_scopings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_application_users_user_id",
                table: "amphibian_elements",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_application_users_user_id",
                table: "botanical_elements",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_application_users_user_id",
                table: "botanical_scopings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_application_users_user_id",
                table: "botanical_surveys",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_application_users_user_id",
                table: "owl_bandings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_protection_zones_application_users_user_id",
                table: "protection_zones",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_application_users_user_id",
                table: "site_callings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_application_users_user_modified_id",
                table: "site_callings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_modified_id",
                table: "site_callings");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "protection_zones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "protection_zones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "permanent_call_stations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "permanent_call_stations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "hex160_required_passes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "hex160_required_passes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_scopings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_scopings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_modified_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_application_users_user_id",
                table: "amphibian_elements",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_application_users_user_id",
                table: "botanical_elements",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_application_users_user_id",
                table: "botanical_scopings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_application_users_user_id",
                table: "botanical_surveys",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_application_users_user_id",
                table: "owl_bandings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_protection_zones_application_users_user_id",
                table: "protection_zones",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_application_users_user_id",
                table: "site_callings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_application_users_user_modified_id",
                table: "site_callings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
