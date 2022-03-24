using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class _20220324StructureUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_BotanicalScopingGuid",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_device_infos_device_info_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_site_calling_repositories_site_calling_reposit~",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_other_wildlife_records_application_users_user_id",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "FK_other_wildlife_records_site_calling_repositories_site_calli~",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "FK_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "FK_user_locations_site_calling_repository_detections_site_call~",
                table: "user_locations");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owl_diagrams_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "site_calling_repository_detections");

            migrationBuilder.DropTable(
                name: "site_calling_repository_tracks");

            migrationBuilder.DropTable(
                name: "site_calling_repositories");

            migrationBuilder.DropIndex(
                name: "IX_user_locations_site_calling_repository_detection_id",
                table: "user_locations");

            migrationBuilder.DropIndex(
                name: "IX_other_wildlife_records_site_calling_repository_id",
                table: "other_wildlife_records");

            migrationBuilder.DropIndex(
                name: "IX_other_wildlife_records_user_id",
                table: "other_wildlife_records");

            migrationBuilder.DropIndex(
                name: "IX_botanical_surveys_BotanicalScopingGuid",
                table: "botanical_surveys");

            migrationBuilder.DropIndex(
                name: "IX_botanical_surveys_device_info_id",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "site_calling_repository_detection_id",
                table: "user_locations");

            migrationBuilder.DropColumn(
                name: "site_calling_repository_id",
                table: "other_wildlife_records");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "other_wildlife_records");

            migrationBuilder.DropColumn(
                name: "BotanicalScopingGuid",
                table: "botanical_surveys");

            migrationBuilder.RenameColumn(
                name: "district_id",
                table: "plant_protection_summaries",
                newName: "region_id");

            migrationBuilder.RenameIndex(
                name: "IX_plant_protection_summaries_district_id",
                table: "plant_protection_summaries",
                newName: "IX_plant_protection_summaries_region_id");

            migrationBuilder.RenameColumn(
                name: "site_calling_repository_id",
                table: "device_infos",
                newName: "botanical_survey_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_site_calling_repository_id",
                table: "device_infos",
                newName: "IX_device_infos_botanical_survey_id");

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "site_callings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "protection_zones",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "permanent_call_stations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "owl_bandings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "hex160_required_passes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "district_id",
                table: "cdfw_spotted_owl_diagrams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "botanical_surveys",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "thp_area_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "botanical_survey_areas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "botanical_scopings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "botanical_elements",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "amphibian_surveys",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "amphibian_elements",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owl_diagrams_district_id",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_botanical_scoping_id",
                table: "botanical_surveys",
                column: "botanical_scoping_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_thp_area_id",
                table: "botanical_surveys",
                column: "thp_area_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id",
                principalTable: "districts",
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
                name: "FK_plant_protection_summaries_regions_region_id",
                table: "plant_protection_summaries",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_plant_protection_summaries_regions_region_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropIndex(
                name: "IX_cdfw_spotted_owl_diagrams_district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropIndex(
                name: "IX_botanical_surveys_botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropIndex(
                name: "IX_botanical_surveys_thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "protection_zones");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "permanent_call_stations");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "owl_bandings");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "hex160_required_passes");

            migrationBuilder.DropColumn(
                name: "district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropColumn(
                name: "botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "botanical_survey_areas");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "botanical_scopings");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "amphibian_elements");

            migrationBuilder.RenameColumn(
                name: "region_id",
                table: "plant_protection_summaries",
                newName: "district_id");

            migrationBuilder.RenameIndex(
                name: "IX_plant_protection_summaries_region_id",
                table: "plant_protection_summaries",
                newName: "IX_plant_protection_summaries_district_id");

            migrationBuilder.RenameColumn(
                name: "botanical_survey_id",
                table: "device_infos",
                newName: "site_calling_repository_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_botanical_survey_id",
                table: "device_infos",
                newName: "IX_device_infos_site_calling_repository_id");

            migrationBuilder.AddColumn<Guid>(
                name: "site_calling_repository_detection_id",
                table: "user_locations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "site_calling_repository_id",
                table: "other_wildlife_records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "other_wildlife_records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BotanicalScopingGuid",
                table: "botanical_surveys",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owl_diagrams_districts",
                schema: "public",
                columns: table => new
                {
                    cdfw_spotted_owl_diagram_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owl_diagrams_districts", x => new { x.cdfw_spotted_owl_diagram_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owl_diagrams_districts_cdfw_spotted_owl_diagra~",
                        column: x => x.cdfw_spotted_owl_diagram_id,
                        principalTable: "cdfw_spotted_owl_diagrams",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owl_diagrams_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repositories",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bird_species_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_description = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    dbh = table.Column<double>(type: "double precision", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    moused = table.Column<bool>(type: "boolean", nullable: false),
                    nest_height = table.Column<double>(type: "double precision", nullable: false),
                    nest_tree = table.Column<bool>(type: "boolean", nullable: false),
                    nest_type = table.Column<string>(type: "text", nullable: true),
                    nesting_status = table.Column<string>(type: "text", nullable: true),
                    occupancy_status = table.Column<string>(type: "text", nullable: false),
                    pz_pass_number = table.Column<int>(type: "integer", nullable: false),
                    pass_number = table.Column<int>(type: "integer", nullable: false),
                    precipitation = table.Column<string>(type: "text", nullable: true),
                    reproductive_status = table.Column<string>(type: "text", nullable: true),
                    site_calling_repository_track_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<string>(type: "text", nullable: true),
                    species_present = table.Column<bool>(type: "boolean", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    starting_lat = table.Column<double>(type: "double precision", nullable: false),
                    starting_lon = table.Column<double>(type: "double precision", nullable: false),
                    sunset_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    survey_type1 = table.Column<string>(type: "text", nullable: false),
                    survey_type2 = table.Column<string>(type: "text", nullable: false),
                    target_species_present = table.Column<bool>(type: "boolean", nullable: false),
                    tree_species = table.Column<string>(type: "text", nullable: true),
                    tree_tagged = table.Column<bool>(type: "boolean", nullable: false),
                    wind = table.Column<string>(type: "text", nullable: true),
                    yearly_activity_center = table.Column<bool>(type: "boolean", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_repositories", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_bird_species_bird_species_survey_~",
                        column: x => x.bird_species_survey_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_protection_zones_preotection_zone~",
                        column: x => x.preotection_zone_id,
                        principalTable: "protection_zones",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repository_detections",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    bird_species_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    age = table.Column<string>(type: "text", nullable: false),
                    bearing = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    detection_lat = table.Column<double>(type: "double precision", nullable: false),
                    detection_lon = table.Column<double>(type: "double precision", nullable: false),
                    detection_method = table.Column<string>(type: "text", nullable: false),
                    detection_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    estimated_location = table.Column<bool>(type: "boolean", nullable: false),
                    female_banding_leg = table.Column<string>(type: "text", nullable: true),
                    female_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    male_banding_leg = table.Column<string>(type: "text", nullable: true),
                    male_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_site = table.Column<string>(type: "text", nullable: true),
                    user_location_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_repository_detections", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_repository_detections_bird_species_bird_specie~",
                        column: x => x.bird_species_found_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repository_detections_site_calling_repositorie~",
                        column: x => x.guid,
                        principalTable: "site_calling_repositories",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repository_tracks",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: true),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_repository_tracks", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_repository_tracks_site_calling_repositories_gu~",
                        column: x => x.guid,
                        principalTable: "site_calling_repositories",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_site_calling_repository_detection_id",
                table: "user_locations",
                column: "site_calling_repository_detection_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_site_calling_repository_id",
                table: "other_wildlife_records",
                column: "site_calling_repository_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_user_id",
                table: "other_wildlife_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_BotanicalScopingGuid",
                table: "botanical_surveys",
                column: "BotanicalScopingGuid");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_device_info_id",
                table: "botanical_surveys",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owl_diagrams_districts_district_id",
                schema: "public",
                table: "cdfw_spotted_owl_diagrams_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_bird_species_survey_id",
                table: "site_calling_repositories",
                column: "bird_species_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_district_id",
                table: "site_calling_repositories",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_hex160_id",
                table: "site_calling_repositories",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_preotection_zone_id",
                table: "site_calling_repositories",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_quad75_id",
                table: "site_calling_repositories",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_user_id",
                table: "site_calling_repositories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_watershed_id",
                table: "site_calling_repositories",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repository_detections_bird_species_found_id",
                table: "site_calling_repository_detections",
                column: "bird_species_found_id");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_BotanicalScopingGuid",
                table: "botanical_surveys",
                column: "BotanicalScopingGuid",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_device_infos_device_info_id",
                table: "botanical_surveys",
                column: "device_info_id",
                principalTable: "device_infos",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_site_calling_repositories_site_calling_reposit~",
                table: "device_infos",
                column: "site_calling_repository_id",
                principalTable: "site_calling_repositories",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_other_wildlife_records_application_users_user_id",
                table: "other_wildlife_records",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_other_wildlife_records_site_calling_repositories_site_calli~",
                table: "other_wildlife_records",
                column: "site_calling_repository_id",
                principalTable: "site_calling_repositories",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_locations_site_calling_repository_detections_site_call~",
                table: "user_locations",
                column: "site_calling_repository_detection_id",
                principalTable: "site_calling_repository_detections",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
