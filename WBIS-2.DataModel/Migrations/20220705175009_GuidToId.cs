using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class GuidToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_amphibian_surveys_amphibian_survey_temp_",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_districts_district_temp_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_hex160s_hex160id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_quad75s_quad75temp_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_districts_district_temp_id1",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_botanical_scopings_botanical_scoping_tem",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_botanical_surveys_botanical_survey_temp_",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_districts_district_temp_id2",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_hex160s_hex160id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_quad75s_quad75temp_id1",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_list_plant_species_plant_species_temp_id",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_of_interest_botanical_elements_guid",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_of_interest_plant_species_plant_species_te",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_points_of_interest_botanical_elements_guid",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scoping_species_plant_species_plant_species_temp_",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_regions_region_temp_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_thp_areas_thp_area_temp_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_districts_district_temp_id3",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_thp_areas_thp_area_temp_id1",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_districts_district_temp_id4",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_thp_areas_thp_area_temp_id2",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owl_diagrams_districts_district_temp_id5",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owls_districts_district_temp_id6",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owls_hex160s_hex160id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owls_quad75s_quad75temp_id2",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_plant_species_plant_species_temp_id3",
                table: "cnddb_occurrences");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_plant_species_plant_species_temp_id4",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_quad75s_quad75temp_id3",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_district_extended_geometries_districts_guid",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "fk_flowering_timelines_plant_species_plant_species_temp_id5",
                table: "flowering_timelines");

            migrationBuilder.DropForeignKey(
                name: "fk_other_wildlife_records_site_callings_site_calling_temp_id1",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "fk_other_wildlife_records_wildlife_species_wildlife_species_te",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_protection_zones_protection_zone_temp_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_quad75s_quad75temp_id4",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_pictures_site_callings_site_calling_temp_id2",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "fk_plant_protection_summaries_plant_species_plant_species_temp",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "fk_regional_plant_lists_regions_region_temp_id1",
                table: "regional_plant_lists");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_site_callings_site_calling_temp_id3",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_user_locations_user_location_temp_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks");

            migrationBuilder.DropColumn(
                name: "email",
                table: "application_users");

            migrationBuilder.DropColumn(
                name: "password_time_stamp",
                table: "application_users");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "wildlife_species",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "watersheds",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "user_map_layers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "user_locations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "user_flex_records",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "thp_areas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "spi_wildlife_sightings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "spi_spows",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "spi_plant_polygons",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "spi_plant_points",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "spi_nogos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "spi_ggows",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "site_callings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "site_calling_tracks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "site_calling_detections",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "regions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "regional_plant_lists",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "quad75s",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "protection_zones",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "plant_species",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "plant_protection_summaries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "pictures",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "permanent_call_stations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "owl_bandings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "other_wildlife_records",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "hex500s",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "hex160s",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "hex160_required_passes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "flowering_timelines",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "districts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "district_extended_geometries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "device_infos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "deleted_geometries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "cnddb_quad_elements",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "cnddb_occurrences",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "cdfw_spotted_owls",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "cdfw_spotted_owl_diagrams",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_surveys",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_survey_areas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_scopings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_scoping_species",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_points_of_interest",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_plants_of_interest",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_plants_list",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "botanical_elements",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "bird_species",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "application_users",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "place_holder",
                table: "application_users",
                newName: "require_password_change");

            migrationBuilder.RenameColumn(
                name: "password_sha",
                table: "application_users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "hint",
                table: "application_users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "_delete",
                table: "application_users",
                newName: "is_application_administrator");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "application_users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "application_groups",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "amphibian_surveys",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "amphibian_species",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "amphibian_points_of_interest",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "amphibian_locations_found",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "amphibian_elements",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "oid",
                table: "application_users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_districts_district_id",
                table: "amphibian_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_locations_found_amphibian_elements_id",
                table: "amphibian_locations_found",
                column: "id",
                principalTable: "amphibian_elements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_elements_id",
                table: "amphibian_points_of_interest",
                column: "id",
                principalTable: "amphibian_elements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_districts_district_id",
                table: "botanical_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_list_botanical_elements_id",
                table: "botanical_plants_list",
                column: "id",
                principalTable: "botanical_elements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_list_plant_species_plant_species_id",
                table: "botanical_plants_list",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_of_interest_botanical_elements_id",
                table: "botanical_plants_of_interest",
                column: "id",
                principalTable: "botanical_elements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_of_interest_plant_species_plant_species_id",
                table: "botanical_plants_of_interest",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_points_of_interest_botanical_elements_id",
                table: "botanical_points_of_interest",
                column: "id",
                principalTable: "botanical_elements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scoping_species_plant_species_plant_species_id",
                table: "botanical_scoping_species",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_regions_region_id",
                table: "botanical_scopings",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_thp_areas_thp_area_id",
                table: "botanical_scopings",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_districts_district_id",
                table: "botanical_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_plant_species_plant_species_id",
                table: "cnddb_occurrences",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_quad_elements_plant_species_plant_species_id",
                table: "cnddb_quad_elements",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_quad_elements_quad75s_quad75_id",
                table: "cnddb_quad_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_district_extended_geometries_districts_id",
                table: "district_extended_geometries",
                column: "id",
                principalTable: "districts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_flowering_timelines_plant_species_plant_species_id",
                table: "flowering_timelines",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_other_wildlife_records_site_callings_site_calling_id",
                table: "other_wildlife_records",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_other_wildlife_records_wildlife_species_wildlife_species_id",
                table: "other_wildlife_records",
                column: "wildlife_species_id",
                principalTable: "wildlife_species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_protection_zones_protection_zone_id",
                table: "owl_bandings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_pictures_site_callings_site_calling_id",
                table: "pictures",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_plant_protection_summaries_plant_species_plant_species_id",
                table: "plant_protection_summaries",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_regional_plant_lists_regions_region_id",
                table: "regional_plant_lists",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_site_callings_site_calling_id",
                table: "site_calling_detections",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_user_locations_user_location_id",
                table: "site_calling_detections",
                column: "user_location_id",
                principalTable: "user_locations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_tracks_site_callings_id",
                table: "site_calling_tracks",
                column: "id",
                principalTable: "site_callings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_districts_district_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_locations_found_amphibian_elements_id",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_elements_id",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_districts_district_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_list_botanical_elements_id",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_list_plant_species_plant_species_id",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_of_interest_botanical_elements_id",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_of_interest_plant_species_plant_species_id",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_points_of_interest_botanical_elements_id",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scoping_species_plant_species_plant_species_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_regions_region_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_thp_areas_thp_area_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_districts_district_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_plant_species_plant_species_id",
                table: "cnddb_occurrences");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_plant_species_plant_species_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_quad75s_quad75_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_district_extended_geometries_districts_id",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "fk_flowering_timelines_plant_species_plant_species_id",
                table: "flowering_timelines");

            migrationBuilder.DropForeignKey(
                name: "fk_other_wildlife_records_site_callings_site_calling_id",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "fk_other_wildlife_records_wildlife_species_wildlife_species_id",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_protection_zones_protection_zone_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_pictures_site_callings_site_calling_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "fk_plant_protection_summaries_plant_species_plant_species_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "fk_regional_plant_lists_regions_region_id",
                table: "regional_plant_lists");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_site_callings_site_calling_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_user_locations_user_location_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_tracks_site_callings_id",
                table: "site_calling_tracks");

            migrationBuilder.DropColumn(
                name: "oid",
                table: "application_users");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "wildlife_species",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "watersheds",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_map_layers",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_locations",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_flex_records",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "thp_areas",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spi_wildlife_sightings",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spi_spows",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spi_plant_polygons",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spi_plant_points",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spi_nogos",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spi_ggows",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "site_callings",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "site_calling_tracks",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "site_calling_detections",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "regions",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "regional_plant_lists",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "quad75s",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "protection_zones",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "plant_species",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "plant_protection_summaries",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "pictures",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "permanent_call_stations",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "owl_bandings",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "other_wildlife_records",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "hex500s",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "hex160s",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "hex160_required_passes",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "flowering_timelines",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "districts",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "district_extended_geometries",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "device_infos",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "deleted_geometries",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cnddb_quad_elements",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cnddb_occurrences",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cdfw_spotted_owls",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cdfw_spotted_owl_diagrams",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_surveys",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_survey_areas",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_scopings",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_scoping_species",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_points_of_interest",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_plants_of_interest",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_plants_list",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "botanical_elements",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "bird_species",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "require_password_change",
                table: "application_users",
                newName: "place_holder");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "application_users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "application_users",
                newName: "password_sha");

            migrationBuilder.RenameColumn(
                name: "is_application_administrator",
                table: "application_users",
                newName: "_delete");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "application_users",
                newName: "hint");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "application_users",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "application_groups",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "amphibian_surveys",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "amphibian_species",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "amphibian_points_of_interest",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "amphibian_locations_found",
                newName: "guid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "amphibian_elements",
                newName: "guid");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "application_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "password_time_stamp",
                table: "application_users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_amphibian_surveys_amphibian_survey_temp_",
                table: "amphibian_elements",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_districts_district_temp_id",
                table: "amphibian_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_hex160s_hex160id",
                table: "amphibian_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_quad75s_quad75temp_id",
                table: "amphibian_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_districts_district_temp_id1",
                table: "amphibian_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_botanical_scopings_botanical_scoping_tem",
                table: "botanical_elements",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_botanical_surveys_botanical_survey_temp_",
                table: "botanical_elements",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_districts_district_temp_id2",
                table: "botanical_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_hex160s_hex160id",
                table: "botanical_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_quad75s_quad75temp_id1",
                table: "botanical_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_list_plant_species_plant_species_temp_id",
                table: "botanical_plants_list",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_of_interest_botanical_elements_guid",
                table: "botanical_plants_of_interest",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_of_interest_plant_species_plant_species_te",
                table: "botanical_plants_of_interest",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_points_of_interest_botanical_elements_guid",
                table: "botanical_points_of_interest",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scoping_species_plant_species_plant_species_temp_",
                table: "botanical_scoping_species",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_regions_region_temp_id",
                table: "botanical_scopings",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_thp_areas_thp_area_temp_id",
                table: "botanical_scopings",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_districts_district_temp_id3",
                table: "botanical_survey_areas",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_thp_areas_thp_area_temp_id1",
                table: "botanical_survey_areas",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_districts_district_temp_id4",
                table: "botanical_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_thp_areas_thp_area_temp_id2",
                table: "botanical_surveys",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owl_diagrams_districts_district_temp_id5",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owls_districts_district_temp_id6",
                table: "cdfw_spotted_owls",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owls_hex160s_hex160id",
                table: "cdfw_spotted_owls",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cdfw_spotted_owls_quad75s_quad75temp_id2",
                table: "cdfw_spotted_owls",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_plant_species_plant_species_temp_id3",
                table: "cnddb_occurrences",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_quad_elements_plant_species_plant_species_temp_id4",
                table: "cnddb_quad_elements",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_quad_elements_quad75s_quad75temp_id3",
                table: "cnddb_quad_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_district_extended_geometries_districts_guid",
                table: "district_extended_geometries",
                column: "guid",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_flowering_timelines_plant_species_plant_species_temp_id5",
                table: "flowering_timelines",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_other_wildlife_records_site_callings_site_calling_temp_id1",
                table: "other_wildlife_records",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_other_wildlife_records_wildlife_species_wildlife_species_te",
                table: "other_wildlife_records",
                column: "wildlife_species_id",
                principalTable: "wildlife_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_protection_zones_protection_zone_temp_id",
                table: "owl_bandings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_quad75s_quad75temp_id4",
                table: "owl_bandings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_pictures_site_callings_site_calling_temp_id2",
                table: "pictures",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_plant_protection_summaries_plant_species_plant_species_temp",
                table: "plant_protection_summaries",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_regional_plant_lists_regions_region_temp_id1",
                table: "regional_plant_lists",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_site_callings_site_calling_temp_id3",
                table: "site_calling_detections",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_user_locations_user_location_temp_id",
                table: "site_calling_detections",
                column: "user_location_id",
                principalTable: "user_locations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks",
                column: "guid",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
