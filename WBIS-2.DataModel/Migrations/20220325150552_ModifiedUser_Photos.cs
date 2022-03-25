using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ModifiedUser_Photos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_district_extended_geometry_districts_guid",
                table: "district_extended_geometry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_district_extended_geometry",
                table: "district_extended_geometry");

            migrationBuilder.DropColumn(
                name: "date_added",
                table: "other_wildlife_records");

            migrationBuilder.DropColumn(
                name: "date_modified",
                table: "other_wildlife_records");

            migrationBuilder.RenameTable(
                name: "district_extended_geometry",
                newName: "district_extended_geometries");

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "protection_zones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "permanent_call_stations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "hex160_required_passes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_survey_areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_scopings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_district_extended_geometries",
                table: "district_extended_geometries",
                column: "guid");

            migrationBuilder.CreateTable(
                name: "pictures",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    image_data = table.Column<byte[]>(type: "bytea", nullable: true),
                    preview_data = table.Column<byte[]>(type: "bytea", nullable: true),
                    site_calling_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owl_banding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_element_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures", x => x.guid);
                    table.ForeignKey(
                        name: "FK_pictures_amphibian_elements_amphibian_element_id",
                        column: x => x.amphibian_element_id,
                        principalTable: "amphibian_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pictures_botanical_elements_botanical_element_id",
                        column: x => x.botanical_element_id,
                        principalTable: "botanical_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pictures_owl_bandings_owl_banding_id",
                        column: x => x.owl_banding_id,
                        principalTable: "owl_bandings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pictures_site_callings_site_calling_id",
                        column: x => x.site_calling_id,
                        principalTable: "site_callings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_user_modified_id",
                table: "site_callings",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_protection_zones_user_modified_id",
                table: "protection_zones",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_stations_user_modified_id",
                table: "permanent_call_stations",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_user_modified_id",
                table: "owl_bandings",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160_required_passes_user_modified_id",
                table: "hex160_required_passes",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_user_modified_id",
                table: "botanical_surveys",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_user_modified_id",
                table: "botanical_survey_areas",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_user_modified_id",
                table: "botanical_scopings",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_user_modified_id",
                table: "botanical_elements",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_user_modified_id",
                table: "amphibian_surveys",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_user_modified_id",
                table: "amphibian_elements",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_amphibian_element_id",
                table: "pictures",
                column: "amphibian_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_botanical_element_id",
                table: "pictures",
                column: "botanical_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_owl_banding_id",
                table: "pictures",
                column: "owl_banding_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_site_calling_id",
                table: "pictures",
                column: "site_calling_id");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements",
                column: "user_modified_id",
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
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements",
                column: "user_modified_id",
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
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas",
                column: "user_modified_id",
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
                name: "FK_district_extended_geometries_districts_guid",
                table: "district_extended_geometries",
                column: "guid",
                principalTable: "districts",
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
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings",
                column: "user_modified_id",
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
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones",
                column: "user_modified_id",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_district_extended_geometries_districts_guid",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_modified_id",
                table: "site_callings");

            migrationBuilder.DropTable(
                name: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_site_callings_user_modified_id",
                table: "site_callings");

            migrationBuilder.DropIndex(
                name: "IX_protection_zones_user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropIndex(
                name: "IX_permanent_call_stations_user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropIndex(
                name: "IX_owl_bandings_user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropIndex(
                name: "IX_hex160_required_passes_user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropIndex(
                name: "IX_botanical_surveys_user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropIndex(
                name: "IX_botanical_survey_areas_user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropIndex(
                name: "IX_botanical_scopings_user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropIndex(
                name: "IX_botanical_elements_user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_surveys_user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_elements_user_modified_id",
                table: "amphibian_elements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_district_extended_geometries",
                table: "district_extended_geometries");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "amphibian_elements");

            migrationBuilder.RenameTable(
                name: "district_extended_geometries",
                newName: "district_extended_geometry");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_added",
                table: "other_wildlife_records",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_modified",
                table: "other_wildlife_records",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_district_extended_geometry",
                table: "district_extended_geometry",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_district_extended_geometry_districts_guid",
                table: "district_extended_geometry",
                column: "guid",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
