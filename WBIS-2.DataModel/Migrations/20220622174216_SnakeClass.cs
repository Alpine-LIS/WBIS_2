using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SnakeClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_active_botanical_survey_areas_application_users_application~",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_active_botanical_survey_areas_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_active_hex160s_application_users_application_user_id",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_active_hex160s_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_districts_district_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_hex160s_amphibian_surveys_amphibian_surve~",
                schema: "public",
                table: "amphibian_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_quad75s_amphibian_surveys_amphibian_surve~",
                schema: "public",
                table: "amphibian_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_watersheds_amphibian_surveys_amphibian_su~",
                schema: "public",
                table: "amphibian_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_application_users_application_groups_application_group_id",
                table: "application_users");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_survey_areas_botanical_survey_~",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_surveys_botanical_survey_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_districts_district_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_list_botanical_plants_of_interest_botanica~",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_list_plant_species_plant_species_id",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_of_interest_botanical_elements_guid",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_of_interest_plant_species_plant_species_id",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_guid",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_modified_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scoping_species_botanical_scopings_botanical_scop~",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scoping_species_plant_species_plant_species_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_regions_region_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_thp_areas_thp_area_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_districts_botanical_scopings_botanical_s~",
                schema: "public",
                table: "botanical_scopings_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_districts_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_quad75s_botanical_scopings_botanical_sco~",
                schema: "public",
                table: "botanical_scopings_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_watersheds_botanical_scopings_botanical_~",
                schema: "public",
                table: "botanical_scopings_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scopings_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_hex160s_botanical_survey_areas_botan~",
                schema: "public",
                table: "botanical_survey_areas_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_quad75s_botanical_survey_areas_botan~",
                schema: "public",
                table: "botanical_survey_areas_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_watersheds_botanical_survey_areas_bo~",
                schema: "public",
                table: "botanical_survey_areas_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_survey_areas_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_districts_district_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_hex160s_botanical_surveys_botanical_surve~",
                schema: "public",
                table: "botanical_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_quad75s_botanical_surveys_botanical_surve~",
                schema: "public",
                table: "botanical_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_watersheds_botanical_surveys_botanical_su~",
                schema: "public",
                table: "botanical_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_plant_species_plant_species_id",
                table: "cnddb_occurrences");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_districts_cnddb_occurrences_cnddb_occurre~",
                schema: "public",
                table: "cnddb_occurrences_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_districts_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_hex160s_cnddb_occurrences_cnddb_occurrenc~",
                schema: "public",
                table: "cnddb_occurrences_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_quad75s_cnddb_occurrences_cnddb_occurrenc~",
                schema: "public",
                table: "cnddb_occurrences_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_watersheds_cnddb_occurrences_cnddb_occurr~",
                schema: "public",
                table: "cnddb_occurrences_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_quad_elements_plant_species_plant_species_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_quad_elements_quad75s_quad75_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_quad_elements_districts_cnddb_quad_elements_cnddb_qua~",
                schema: "public",
                table: "cnddb_quad_elements_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_quad_elements_districts_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_data_form_fields_data_forms_data_form_id",
                schema: "flex",
                table: "data_form_fields");

            migrationBuilder.DropForeignKey(
                name: "FK_data_form_fields_template_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields");

            migrationBuilder.DropForeignKey(
                name: "FK_data_forms_templates_template_id",
                schema: "flex",
                table: "data_forms");

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

            migrationBuilder.DropForeignKey(
                name: "FK_district_extended_geometries_districts_guid",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "FK_flowering_timelines_plant_species_plant_species_id",
                table: "flowering_timelines");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_bird_species_bird_species_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160_required_passes_hex160s_hex160_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_current_protection_zone_id",
                table: "hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_districts_districts_district_id",
                schema: "public",
                table: "hex160s_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_districts_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_protection_zones_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_quad75s_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_watersheds_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_hex160s_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_other_wildlife_records_site_callings_site_calling_id",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "FK_other_wildlife_records_wildlife_species_wildlife_species_id",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_bird_species_bird_species_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_districts_district_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_protection_zones_protection_zone_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_permanent_call_stations_hex160s_hex160_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_botanical_elements_botanical_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_owl_bandings_owl_banding_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_site_callings_site_calling_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "FK_plant_protection_summaries_plant_species_plant_species_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_protection_zones_application_users_user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "FK_quad75s_districts_districts_district_id",
                schema: "public",
                table: "quad75s_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_quad75s_districts_quad75s_quad75_id",
                schema: "public",
                table: "quad75s_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_regional_plant_lists_plant_species_plant_species_id",
                table: "regional_plant_lists");

            migrationBuilder.DropForeignKey(
                name: "FK_regional_plant_lists_regions_region_id",
                table: "regional_plant_lists");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_application_users_user_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_application_users_user_modified_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_bird_species_bird_species_found_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_districts_district_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_hex160s_hex160_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_hex500s_hex500_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_quad75s_quad75_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_site_callings_site_calling_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_user_locations_user_location_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_watersheds_watershed_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_application_users_user_modified_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_bird_species_bird_species_survey_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_districts_district_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_hex160s_hex160_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_hex500s_hex500_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_protection_zones_protection_zone_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_quad75s_quad75_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_watersheds_watershed_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_ggows_districts_district_id",
                table: "spi_ggows");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_ggows_watersheds_watershed_id",
                table: "spi_ggows");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_nogos_districts_district_id",
                table: "spi_nogos");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_nogos_watersheds_watershed_id",
                table: "spi_nogos");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_districts_district_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_plant_species_plant_species_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_plant_species_plant_species_id",
                table: "spi_plant_polygons");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_hex160s_spi_plant_polygons_spi_plant_pol~",
                schema: "public",
                table: "spi_plant_polygons_hex160s");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_quad75s_spi_plant_polygons_spi_plant_pol~",
                schema: "public",
                table: "spi_plant_polygons_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_watersheds_spi_plant_polygons_spi_plant_~",
                schema: "public",
                table: "spi_plant_polygons_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_polygons_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_spows_districts_district_id",
                table: "spi_spows");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_spows_watersheds_watershed_id",
                table: "spi_spows");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_wildlife_sightings_districts_district_id",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_wildlife_sightings_watersheds_watershed_id",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropForeignKey(
                name: "FK_template_fields_templates_template_id",
                schema: "flex",
                table: "template_fields");

            migrationBuilder.DropForeignKey(
                name: "FK_user_flex_records_application_users_user_id",
                table: "user_flex_records");

            migrationBuilder.DropForeignKey(
                name: "FK_user_flex_records_application_users_user_modified_id",
                table: "user_flex_records");

            migrationBuilder.DropForeignKey(
                name: "FK_user_flex_records_data_forms_data_form_id",
                table: "user_flex_records");

            migrationBuilder.DropForeignKey(
                name: "FK_user_map_layers_application_users_application_user_id",
                table: "user_map_layers");

            migrationBuilder.DropForeignKey(
                name: "FK_users_districts_application_users_application_user_id",
                schema: "public",
                table: "users_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_users_districts_districts_district_id",
                schema: "public",
                table: "users_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_districts_districts_district_id",
                schema: "public",
                table: "watersheds_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_districts_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_districts");

            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_wildlife_species",
                table: "wildlife_species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_watersheds_districts",
                schema: "public",
                table: "watersheds_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_watersheds",
                table: "watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users_districts",
                schema: "public",
                table: "users_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_map_layers",
                table: "user_map_layers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_locations",
                table: "user_locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_flex_records",
                table: "user_flex_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_thp_areas",
                table: "thp_areas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_templates",
                schema: "flex",
                table: "templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_template_fields",
                schema: "flex",
                table: "template_fields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_wildlife_sightings",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_spows",
                table: "spi_spows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_plant_polygons_watersheds",
                schema: "public",
                table: "spi_plant_polygons_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_plant_polygons_quad75s",
                schema: "public",
                table: "spi_plant_polygons_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_plant_polygons_hex160s",
                schema: "public",
                table: "spi_plant_polygons_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_plant_polygons",
                table: "spi_plant_polygons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_plant_points",
                table: "spi_plant_points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_nogos",
                table: "spi_nogos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_spi_ggows",
                table: "spi_ggows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_site_callings",
                table: "site_callings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_site_calling_tracks",
                table: "site_calling_tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_site_calling_detections",
                table: "site_calling_detections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regions",
                table: "regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regional_plant_lists",
                table: "regional_plant_lists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quad75s_districts",
                schema: "public",
                table: "quad75s_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quad75s",
                table: "quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_protection_zones",
                table: "protection_zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plant_species",
                table: "plant_species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plant_protection_summaries",
                table: "plant_protection_summaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pictures",
                table: "pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permanent_call_stations",
                table: "permanent_call_stations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_owl_bandings",
                table: "owl_bandings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_other_wildlife_records",
                table: "other_wildlife_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex500s",
                table: "hex500s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex160s_watersheds",
                schema: "public",
                table: "hex160s_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex160s_quad75s",
                schema: "public",
                table: "hex160s_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex160s_protection_zones",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex160s_districts",
                schema: "public",
                table: "hex160s_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex160s",
                table: "hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hex160_required_passes",
                table: "hex160_required_passes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flowering_timelines",
                table: "flowering_timelines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_districts",
                table: "districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_district_extended_geometries",
                table: "district_extended_geometries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_device_infos",
                table: "device_infos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deleted_geometries",
                table: "deleted_geometries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_forms",
                schema: "flex",
                table: "data_forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_form_fields",
                schema: "flex",
                table: "data_form_fields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_quad_elements_districts",
                schema: "public",
                table: "cnddb_quad_elements_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_quad_elements",
                table: "cnddb_quad_elements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_occurrences_watersheds",
                schema: "public",
                table: "cnddb_occurrences_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_occurrences_quad75s",
                schema: "public",
                table: "cnddb_occurrences_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_occurrences_hex160s",
                schema: "public",
                table: "cnddb_occurrences_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_occurrences_districts",
                schema: "public",
                table: "cnddb_occurrences_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cnddb_occurrences",
                table: "cnddb_occurrences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cdfw_spotted_owls",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cdfw_spotted_owl_diagrams",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_surveys_watersheds",
                schema: "public",
                table: "botanical_surveys_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_surveys_quad75s",
                schema: "public",
                table: "botanical_surveys_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_surveys_hex160s",
                schema: "public",
                table: "botanical_surveys_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_surveys",
                table: "botanical_surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_survey_areas_watersheds",
                schema: "public",
                table: "botanical_survey_areas_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_survey_areas_quad75s",
                schema: "public",
                table: "botanical_survey_areas_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_survey_areas_hex160s",
                schema: "public",
                table: "botanical_survey_areas_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_survey_areas",
                table: "botanical_survey_areas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_scopings_watersheds",
                schema: "public",
                table: "botanical_scopings_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_scopings_quad75s",
                schema: "public",
                table: "botanical_scopings_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_scopings_districts",
                schema: "public",
                table: "botanical_scopings_districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_scopings",
                table: "botanical_scopings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_scoping_species",
                table: "botanical_scoping_species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_points_of_interest",
                table: "botanical_points_of_interest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_plants_of_interest",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_plants_list",
                table: "botanical_plants_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_botanical_elements",
                table: "botanical_elements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bird_species",
                table: "bird_species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_application_users",
                table: "application_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_application_groups",
                table: "application_groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_surveys_watersheds",
                schema: "public",
                table: "amphibian_surveys_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_surveys_quad75s",
                schema: "public",
                table: "amphibian_surveys_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_surveys_hex160s",
                schema: "public",
                table: "amphibian_surveys_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_surveys",
                table: "amphibian_surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_species",
                table: "amphibian_species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_points_of_interest",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_locations_found",
                table: "amphibian_locations_found");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amphibian_elements",
                table: "amphibian_elements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_active_hex160s",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_active_botanical_survey_areas",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.RenameIndex(
                name: "IX_watersheds_quad75s_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                newName: "ix_watersheds_quad75s_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_watersheds_districts_watershed_id",
                schema: "public",
                table: "watersheds_districts",
                newName: "ix_watersheds_districts_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_districts_district_id",
                schema: "public",
                table: "users_districts",
                newName: "ix_users_districts_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_map_layers_application_user_id",
                table: "user_map_layers",
                newName: "ix_user_map_layers_application_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_flex_records_user_modified_id",
                table: "user_flex_records",
                newName: "ix_user_flex_records_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_flex_records_user_id",
                table: "user_flex_records",
                newName: "ix_user_flex_records_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_flex_records_data_form_id",
                table: "user_flex_records",
                newName: "ix_user_flex_records_data_form_id");

            migrationBuilder.RenameIndex(
                name: "IX_template_fields_template_id",
                schema: "flex",
                table: "template_fields",
                newName: "ix_template_fields_template_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_wildlife_sightings_watershed_id",
                table: "spi_wildlife_sightings",
                newName: "ix_spi_wildlife_sightings_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_wildlife_sightings_district_id",
                table: "spi_wildlife_sightings",
                newName: "ix_spi_wildlife_sightings_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_spows_watershed_id",
                table: "spi_spows",
                newName: "ix_spi_spows_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_spows_district_id",
                table: "spi_spows",
                newName: "ix_spi_spows_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_polygons_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                newName: "ix_spi_plant_polygons_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_polygons_quad75s_spi_plant_polygon_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                newName: "ix_spi_plant_polygons_quad75s_spi_plant_polygon_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_polygons_hex160s_spi_plant_polygon_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                newName: "ix_spi_plant_polygons_hex160s_spi_plant_polygon_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_polygons_plant_species_id",
                table: "spi_plant_polygons",
                newName: "ix_spi_plant_polygons_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_polygons_district_id",
                table: "spi_plant_polygons",
                newName: "ix_spi_plant_polygons_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_points_watershed_id",
                table: "spi_plant_points",
                newName: "ix_spi_plant_points_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_points_quad75_id",
                table: "spi_plant_points",
                newName: "ix_spi_plant_points_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_points_plant_species_id",
                table: "spi_plant_points",
                newName: "ix_spi_plant_points_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_points_hex160_id",
                table: "spi_plant_points",
                newName: "ix_spi_plant_points_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_plant_points_district_id",
                table: "spi_plant_points",
                newName: "ix_spi_plant_points_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_nogos_watershed_id",
                table: "spi_nogos",
                newName: "ix_spi_nogos_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_nogos_district_id",
                table: "spi_nogos",
                newName: "ix_spi_nogos_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_ggows_watershed_id",
                table: "spi_ggows",
                newName: "ix_spi_ggows_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_spi_ggows_district_id",
                table: "spi_ggows",
                newName: "ix_spi_ggows_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_watershed_id",
                table: "site_callings",
                newName: "ix_site_callings_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_user_modified_id",
                table: "site_callings",
                newName: "ix_site_callings_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_user_id",
                table: "site_callings",
                newName: "ix_site_callings_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_quad75_id",
                table: "site_callings",
                newName: "ix_site_callings_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_protection_zone_id",
                table: "site_callings",
                newName: "ix_site_callings_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_hex500_id",
                table: "site_callings",
                newName: "ix_site_callings_hex500_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_hex160_id",
                table: "site_callings",
                newName: "ix_site_callings_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_district_id",
                table: "site_callings",
                newName: "ix_site_callings_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_callings_bird_species_survey_id",
                table: "site_callings",
                newName: "ix_site_callings_bird_species_survey_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_watershed_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_user_modified_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_user_location_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_user_location_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_user_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_site_calling_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_quad75_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_hex500_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_hex500_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_hex160_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_district_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_site_calling_detections_bird_species_found_id",
                table: "site_calling_detections",
                newName: "ix_site_calling_detections_bird_species_found_id");

            migrationBuilder.RenameIndex(
                name: "IX_regional_plant_lists_region_id",
                table: "regional_plant_lists",
                newName: "ix_regional_plant_lists_region_id");

            migrationBuilder.RenameIndex(
                name: "IX_regional_plant_lists_plant_species_id",
                table: "regional_plant_lists",
                newName: "ix_regional_plant_lists_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_quad75s_districts_quad75_id",
                schema: "public",
                table: "quad75s_districts",
                newName: "ix_quad75s_districts_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_protection_zones_user_modified_id",
                table: "protection_zones",
                newName: "ix_protection_zones_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_protection_zones_user_id",
                table: "protection_zones",
                newName: "ix_protection_zones_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_plant_protection_summaries_plant_species_id",
                table: "plant_protection_summaries",
                newName: "ix_plant_protection_summaries_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_plant_protection_summaries_district_id",
                table: "plant_protection_summaries",
                newName: "ix_plant_protection_summaries_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_site_calling_id",
                table: "pictures",
                newName: "ix_pictures_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_owl_banding_id",
                table: "pictures",
                newName: "ix_pictures_owl_banding_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_botanical_element_id",
                table: "pictures",
                newName: "ix_pictures_botanical_element_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_amphibian_element_id",
                table: "pictures",
                newName: "ix_pictures_amphibian_element_id");

            migrationBuilder.RenameIndex(
                name: "IX_permanent_call_stations_user_modified_id",
                table: "permanent_call_stations",
                newName: "ix_permanent_call_stations_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_permanent_call_stations_user_id",
                table: "permanent_call_stations",
                newName: "ix_permanent_call_stations_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_permanent_call_stations_hex160_id",
                table: "permanent_call_stations",
                newName: "ix_permanent_call_stations_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_watershed_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_user_modified_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_user_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_quad75_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_protection_zone_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_hex160_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_district_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_owl_bandings_bird_species_id",
                table: "owl_bandings",
                newName: "ix_owl_bandings_bird_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_other_wildlife_records_wildlife_species_id",
                table: "other_wildlife_records",
                newName: "ix_other_wildlife_records_wildlife_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_other_wildlife_records_site_calling_id",
                table: "other_wildlife_records",
                newName: "ix_other_wildlife_records_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds",
                newName: "ix_hex160s_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s",
                newName: "ix_hex160s_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                newName: "ix_hex160s_protection_zones_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_districts_hex160_id",
                schema: "public",
                table: "hex160s_districts",
                newName: "ix_hex160s_districts_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160s_current_protection_zone_id",
                table: "hex160s",
                newName: "ix_hex160s_current_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160_required_passes_user_modified_id",
                table: "hex160_required_passes",
                newName: "ix_hex160_required_passes_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160_required_passes_user_id",
                table: "hex160_required_passes",
                newName: "ix_hex160_required_passes_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160_required_passes_hex160_id",
                table: "hex160_required_passes",
                newName: "ix_hex160_required_passes_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_hex160_required_passes_bird_species_id",
                table: "hex160_required_passes",
                newName: "ix_hex160_required_passes_bird_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_flowering_timelines_plant_species_id",
                table: "flowering_timelines",
                newName: "ix_flowering_timelines_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_site_calling_id",
                table: "device_infos",
                newName: "ix_device_infos_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_owl_banding_id",
                table: "device_infos",
                newName: "ix_device_infos_owl_banding_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_botanical_survey_id",
                table: "device_infos",
                newName: "ix_device_infos_botanical_survey_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_botanical_element_id",
                table: "device_infos",
                newName: "ix_device_infos_botanical_element_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_amphibian_survey_id",
                table: "device_infos",
                newName: "ix_device_infos_amphibian_survey_id");

            migrationBuilder.RenameIndex(
                name: "IX_device_infos_amphibian_element_id",
                table: "device_infos",
                newName: "ix_device_infos_amphibian_element_id");

            migrationBuilder.RenameIndex(
                name: "IX_data_forms_template_id",
                schema: "flex",
                table: "data_forms",
                newName: "ix_data_forms_template_id");

            migrationBuilder.RenameIndex(
                name: "IX_data_form_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields",
                newName: "ix_data_form_fields_template_field_id");

            migrationBuilder.RenameIndex(
                name: "IX_data_form_fields_data_form_id",
                schema: "flex",
                table: "data_form_fields",
                newName: "ix_data_form_fields_data_form_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_quad_elements_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                newName: "ix_cnddb_quad_elements_districts_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_quad_elements_quad75_id",
                table: "cnddb_quad_elements",
                newName: "ix_cnddb_quad_elements_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_quad_elements_plant_species_id",
                table: "cnddb_quad_elements",
                newName: "ix_cnddb_quad_elements_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_occurrences_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                newName: "ix_cnddb_occurrences_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_occurrences_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                newName: "ix_cnddb_occurrences_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_occurrences_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                newName: "ix_cnddb_occurrences_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_occurrences_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts",
                newName: "ix_cnddb_occurrences_districts_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_cnddb_occurrences_plant_species_id",
                table: "cnddb_occurrences",
                newName: "ix_cnddb_occurrences_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_cdfw_spotted_owls_watershed_id",
                table: "cdfw_spotted_owls",
                newName: "ix_cdfw_spotted_owls_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_cdfw_spotted_owls_quad75_id",
                table: "cdfw_spotted_owls",
                newName: "ix_cdfw_spotted_owls_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_cdfw_spotted_owls_hex160_id",
                table: "cdfw_spotted_owls",
                newName: "ix_cdfw_spotted_owls_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_cdfw_spotted_owls_district_id",
                table: "cdfw_spotted_owls",
                newName: "ix_cdfw_spotted_owls_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_cdfw_spotted_owl_diagrams_district_id",
                table: "cdfw_spotted_owl_diagrams",
                newName: "ix_cdfw_spotted_owl_diagrams_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds",
                newName: "ix_botanical_surveys_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s",
                newName: "ix_botanical_surveys_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s",
                newName: "ix_botanical_surveys_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_user_modified_id",
                table: "botanical_surveys",
                newName: "ix_botanical_surveys_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_user_id",
                table: "botanical_surveys",
                newName: "ix_botanical_surveys_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_thp_area_id",
                table: "botanical_surveys",
                newName: "ix_botanical_surveys_thp_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_district_id",
                table: "botanical_surveys",
                newName: "ix_botanical_surveys_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_botanical_survey_area_id",
                table: "botanical_surveys",
                newName: "ix_botanical_surveys_botanical_survey_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_surveys_botanical_scoping_id",
                table: "botanical_surveys",
                newName: "ix_botanical_surveys_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                newName: "ix_botanical_survey_areas_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                newName: "ix_botanical_survey_areas_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                newName: "ix_botanical_survey_areas_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_user_modified_id",
                table: "botanical_survey_areas",
                newName: "ix_botanical_survey_areas_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_user_id",
                table: "botanical_survey_areas",
                newName: "ix_botanical_survey_areas_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_thp_area_id",
                table: "botanical_survey_areas",
                newName: "ix_botanical_survey_areas_thp_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_district_id",
                table: "botanical_survey_areas",
                newName: "ix_botanical_survey_areas_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_survey_areas_botanical_scoping_id",
                table: "botanical_survey_areas",
                newName: "ix_botanical_survey_areas_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds",
                newName: "ix_botanical_scopings_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s",
                newName: "ix_botanical_scopings_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts",
                newName: "ix_botanical_scopings_districts_district_id");

            migrationBuilder.RenameColumn(
                name: "Forester",
                table: "botanical_scopings",
                newName: "forester");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_user_modified_id",
                table: "botanical_scopings",
                newName: "ix_botanical_scopings_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_user_id",
                table: "botanical_scopings",
                newName: "ix_botanical_scopings_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_thp_area_id",
                table: "botanical_scopings",
                newName: "ix_botanical_scopings_thp_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scopings_region_id",
                table: "botanical_scopings",
                newName: "ix_botanical_scopings_region_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scoping_species_user_modified_id",
                table: "botanical_scoping_species",
                newName: "ix_botanical_scoping_species_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scoping_species_user_id",
                table: "botanical_scoping_species",
                newName: "ix_botanical_scoping_species_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scoping_species_plant_species_id",
                table: "botanical_scoping_species",
                newName: "ix_botanical_scoping_species_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_scoping_species_botanical_scoping_id",
                table: "botanical_scoping_species",
                newName: "ix_botanical_scoping_species_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_plants_of_interest_plant_species_id",
                table: "botanical_plants_of_interest",
                newName: "ix_botanical_plants_of_interest_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_plants_list_plant_species_id",
                table: "botanical_plants_list",
                newName: "ix_botanical_plants_list_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_plants_list_botanical_plant_of_interest_id",
                table: "botanical_plants_list",
                newName: "ix_botanical_plants_list_botanical_plant_of_interest_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_watershed_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_user_modified_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_user_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_quad75_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_hex160_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_district_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_botanical_survey_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_botanical_survey_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_botanical_survey_area_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_botanical_survey_area_id");

            migrationBuilder.RenameIndex(
                name: "IX_botanical_elements_botanical_scoping_id",
                table: "botanical_elements",
                newName: "ix_botanical_elements_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "IX_application_users_application_group_id",
                table: "application_users",
                newName: "ix_application_users_application_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_surveys_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                newName: "ix_amphibian_surveys_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_surveys_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                newName: "ix_amphibian_surveys_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_surveys_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                newName: "ix_amphibian_surveys_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_surveys_user_modified_id",
                table: "amphibian_surveys",
                newName: "ix_amphibian_surveys_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_surveys_user_id",
                table: "amphibian_surveys",
                newName: "ix_amphibian_surveys_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_surveys_district_id",
                table: "amphibian_surveys",
                newName: "ix_amphibian_surveys_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_points_of_interest_other_wildlife_id",
                table: "amphibian_points_of_interest",
                newName: "ix_amphibian_points_of_interest_other_wildlife_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_locations_found_amphibian_species_id",
                table: "amphibian_locations_found",
                newName: "ix_amphibian_locations_found_amphibian_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_watershed_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_watershed_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_user_modified_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_user_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_quad75_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_quad75_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_hex160_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_hex160_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_district_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_district_id");

            migrationBuilder.RenameIndex(
                name: "IX_amphibian_elements_amphibian_survey_id",
                table: "amphibian_elements",
                newName: "ix_amphibian_elements_amphibian_survey_id");

            migrationBuilder.RenameIndex(
                name: "IX_active_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s",
                newName: "ix_active_hex160s_unit_id");

            migrationBuilder.RenameIndex(
                name: "IX_active_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                newName: "ix_active_botanical_survey_areas_unit_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_wildlife_species",
                table: "wildlife_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s",
                columns: new[] { "quad75_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_watersheds_districts",
                schema: "public",
                table: "watersheds_districts",
                columns: new[] { "district_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_watersheds",
                table: "watersheds",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users_districts",
                schema: "public",
                table: "users_districts",
                columns: new[] { "application_user_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_map_layers",
                table: "user_map_layers",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_locations",
                table: "user_locations",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_flex_records",
                table: "user_flex_records",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_thp_areas",
                table: "thp_areas",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_templates",
                schema: "flex",
                table: "templates",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_template_fields",
                schema: "flex",
                table: "template_fields",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_wildlife_sightings",
                table: "spi_wildlife_sightings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_spows",
                table: "spi_spows",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_plant_polygons_watersheds",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                columns: new[] { "spi_plant_polygon_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_plant_polygons_quad75s",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                columns: new[] { "quad75_id", "spi_plant_polygon_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_plant_polygons_hex160s",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                columns: new[] { "hex160_id", "spi_plant_polygon_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_plant_polygons",
                table: "spi_plant_polygons",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_plant_points",
                table: "spi_plant_points",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_nogos",
                table: "spi_nogos",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spi_ggows",
                table: "spi_ggows",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_site_callings",
                table: "site_callings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_site_calling_tracks",
                table: "site_calling_tracks",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_site_calling_detections",
                table: "site_calling_detections",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_regions",
                table: "regions",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_regional_plant_lists",
                table: "regional_plant_lists",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_quad75s_districts",
                schema: "public",
                table: "quad75s_districts",
                columns: new[] { "district_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_quad75s",
                table: "quad75s",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_protection_zones",
                table: "protection_zones",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_plant_species",
                table: "plant_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_plant_protection_summaries",
                table: "plant_protection_summaries",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_pictures",
                table: "pictures",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_permanent_call_stations",
                table: "permanent_call_stations",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_owl_bandings",
                table: "owl_bandings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_other_wildlife_records",
                table: "other_wildlife_records",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex500s",
                table: "hex500s",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex160s_watersheds",
                schema: "public",
                table: "hex160s_watersheds",
                columns: new[] { "hex160_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex160s_quad75s",
                schema: "public",
                table: "hex160s_quad75s",
                columns: new[] { "hex160_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex160s_protection_zones",
                schema: "public",
                table: "hex160s_protection_zones",
                columns: new[] { "hex160_id", "protection_zone_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex160s_districts",
                schema: "public",
                table: "hex160s_districts",
                columns: new[] { "district_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex160s",
                table: "hex160s",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_hex160_required_passes",
                table: "hex160_required_passes",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_flowering_timelines",
                table: "flowering_timelines",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_districts",
                table: "districts",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_district_extended_geometries",
                table: "district_extended_geometries",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_device_infos",
                table: "device_infos",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_deleted_geometries",
                table: "deleted_geometries",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_data_forms",
                schema: "flex",
                table: "data_forms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_data_form_fields",
                schema: "flex",
                table: "data_form_fields",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_quad_elements_districts",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                columns: new[] { "cnddb_quad_element_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_quad_elements",
                table: "cnddb_quad_elements",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_occurrences_watersheds",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                columns: new[] { "cnddb_occurrence_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_occurrences_quad75s",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                columns: new[] { "cnddb_occurrence_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_occurrences_hex160s",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                columns: new[] { "cnddb_occurrence_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_occurrences_districts",
                schema: "public",
                table: "cnddb_occurrences_districts",
                columns: new[] { "cnddb_occurrence_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnddb_occurrences",
                table: "cnddb_occurrences",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cdfw_spotted_owls",
                table: "cdfw_spotted_owls",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cdfw_spotted_owl_diagrams",
                table: "cdfw_spotted_owl_diagrams",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_surveys_watersheds",
                schema: "public",
                table: "botanical_surveys_watersheds",
                columns: new[] { "botanical_survey_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_surveys_quad75s",
                schema: "public",
                table: "botanical_surveys_quad75s",
                columns: new[] { "botanical_survey_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_surveys_hex160s",
                schema: "public",
                table: "botanical_surveys_hex160s",
                columns: new[] { "botanical_survey_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_surveys",
                table: "botanical_surveys",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_survey_areas_watersheds",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                columns: new[] { "botanical_survey_area_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_survey_areas_quad75s",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                columns: new[] { "botanical_survey_area_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_survey_areas_hex160s",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                columns: new[] { "botanical_survey_area_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_survey_areas",
                table: "botanical_survey_areas",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_scopings_watersheds",
                schema: "public",
                table: "botanical_scopings_watersheds",
                columns: new[] { "botanical_scoping_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_scopings_quad75s",
                schema: "public",
                table: "botanical_scopings_quad75s",
                columns: new[] { "botanical_scoping_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_scopings_districts",
                schema: "public",
                table: "botanical_scopings_districts",
                columns: new[] { "botanical_scoping_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_scopings",
                table: "botanical_scopings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_scoping_species",
                table: "botanical_scoping_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_points_of_interest",
                table: "botanical_points_of_interest",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_plants_of_interest",
                table: "botanical_plants_of_interest",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_plants_list",
                table: "botanical_plants_list",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_botanical_elements",
                table: "botanical_elements",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_bird_species",
                table: "bird_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_application_users",
                table: "application_users",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_application_groups",
                table: "application_groups",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_surveys_watersheds",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                columns: new[] { "amphibian_survey_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_surveys_quad75s",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                columns: new[] { "amphibian_survey_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_surveys_hex160s",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                columns: new[] { "amphibian_survey_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_surveys",
                table: "amphibian_surveys",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_species",
                table: "amphibian_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_points_of_interest",
                table: "amphibian_points_of_interest",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_locations_found",
                table: "amphibian_locations_found",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_amphibian_elements",
                table: "amphibian_elements",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_active_hex160s",
                schema: "public",
                table: "active_hex160s",
                columns: new[] { "application_user_id", "unit_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_active_botanical_survey_areas",
                schema: "public",
                table: "active_botanical_survey_areas",
                columns: new[] { "application_user_id", "unit_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_active_botanical_survey_areas_application_users_application",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_active_botanical_survey_areas_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "unit_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_active_hex160s_application_users_application_user_id",
                schema: "public",
                table: "active_hex160s",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_active_hex160s_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s",
                column: "unit_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_amphibian_surveys_amphibian_survey_temp_",
                table: "amphibian_elements",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_application_users_user_id",
                table: "amphibian_elements",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_elements_application_users_user_modified_id",
                table: "amphibian_elements",
                column: "user_modified_id",
                principalTable: "application_users",
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
                name: "fk_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_locations_found_amphibian_species_amphibian_speci",
                table: "amphibian_locations_found",
                column: "amphibian_species_id",
                principalTable: "amphibian_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_species_other_wildli",
                table: "amphibian_points_of_interest",
                column: "other_wildlife_id",
                principalTable: "amphibian_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_districts_district_temp_id1",
                table: "amphibian_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_hex160s_amphibian_surveys_amphibian_surve",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_quad75s_amphibian_surveys_amphibian_surve",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_watersheds_amphibian_surveys_amphibian_su",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_amphibian_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_application_users_application_groups_application_group_id",
                table: "application_users",
                column: "application_group_id",
                principalTable: "application_groups",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_application_users_user_id",
                table: "botanical_elements",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_botanical_scopings_botanical_scoping_tem",
                table: "botanical_elements",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_elements_botanical_survey_areas_botanical_survey_",
                table: "botanical_elements",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
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
                name: "fk_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_plants_list_botanical_plants_of_interest_botanica",
                table: "botanical_plants_list",
                column: "botanical_plant_of_interest_id",
                principalTable: "botanical_plants_of_interest",
                principalColumn: "guid");

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
                name: "fk_botanical_scoping_species_application_users_user_id",
                table: "botanical_scoping_species",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scoping_species_application_users_user_modified_id",
                table: "botanical_scoping_species",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scoping_species_botanical_scopings_botanical_scop",
                table: "botanical_scoping_species",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
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
                name: "fk_botanical_scopings_application_users_user_id",
                table: "botanical_scopings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

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
                name: "fk_botanical_scopings_districts_botanical_scopings_botanical_s",
                schema: "public",
                table: "botanical_scopings_districts",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_districts_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_quad75s_botanical_scopings_botanical_sco",
                schema: "public",
                table: "botanical_scopings_quad75s",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_watersheds_botanical_scopings_botanical_",
                schema: "public",
                table: "botanical_scopings_watersheds",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_scopings_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_botanical_scopings_botanical_scoping",
                table: "botanical_survey_areas",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

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
                name: "fk_botanical_survey_areas_hex160s_botanical_survey_areas_botan",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_quad75s_botanical_survey_areas_botan",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_watersheds_botanical_survey_areas_bo",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_survey_areas_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_application_users_user_id",
                table: "botanical_surveys",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_botanical_survey_areas_botanical_survey_a",
                table: "botanical_surveys",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
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
                name: "fk_botanical_surveys_hex160s_botanical_surveys_botanical_surve",
                schema: "public",
                table: "botanical_surveys_hex160s",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_quad75s_botanical_surveys_botanical_surve",
                schema: "public",
                table: "botanical_surveys_quad75s",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_watersheds_botanical_surveys_botanical_su",
                schema: "public",
                table: "botanical_surveys_watersheds",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_botanical_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "fk_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_plant_species_plant_species_temp_id3",
                table: "cnddb_occurrences",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_districts_cnddb_occurrences_cnddb_occurre",
                schema: "public",
                table: "cnddb_occurrences_districts",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_districts_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_hex160s_cnddb_occurrences_cnddb_occurrenc",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_quad75s_cnddb_occurrences_cnddb_occurrenc",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_watersheds_cnddb_occurrences_cnddb_occurr",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_occurrences_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "fk_cnddb_quad_elements_districts_cnddb_quad_elements_cnddb_qua",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                column: "cnddb_quad_element_id",
                principalTable: "cnddb_quad_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cnddb_quad_elements_districts_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_data_form_fields_data_forms_data_form_id",
                schema: "flex",
                table: "data_form_fields",
                column: "data_form_id",
                principalSchema: "flex",
                principalTable: "data_forms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_data_form_fields_template_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields",
                column: "template_field_id",
                principalSchema: "flex",
                principalTable: "template_fields",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_data_forms_templates_template_id",
                schema: "flex",
                table: "data_forms",
                column: "template_id",
                principalSchema: "flex",
                principalTable: "templates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_device_infos_amphibian_elements_amphibian_element_id",
                table: "device_infos",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_device_infos_amphibian_surveys_amphibian_survey_id",
                table: "device_infos",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_device_infos_botanical_elements_botanical_element_id",
                table: "device_infos",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_device_infos_site_callings_site_calling_id",
                table: "device_infos",
                column: "site_calling_id",
                principalTable: "site_callings",
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
                name: "fk_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_hex160_required_passes_bird_species_bird_species_id",
                table: "hex160_required_passes",
                column: "bird_species_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160_required_passes_hex160s_hex160_id",
                table: "hex160_required_passes",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_protection_zones_current_protection_zone_id",
                table: "hex160s",
                column: "current_protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_districts_districts_district_id",
                schema: "public",
                table: "hex160s_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_districts_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_districts",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_protection_zones_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_protection_zones_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_quad75s_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_quad75s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_watersheds_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_watersheds",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hex160s_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
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
                name: "fk_owl_bandings_application_users_user_id",
                table: "owl_bandings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_bird_species_bird_species_id",
                table: "owl_bandings",
                column: "bird_species_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_districts_district_id",
                table: "owl_bandings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

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
                name: "fk_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_permanent_call_stations_hex160s_hex160_id",
                table: "permanent_call_stations",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_pictures_botanical_elements_botanical_element_id",
                table: "pictures",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_pictures_owl_bandings_owl_banding_id",
                table: "pictures",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_pictures_site_callings_site_calling_temp_id2",
                table: "pictures",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_plant_protection_summaries_plant_species_plant_species_temp",
                table: "plant_protection_summaries",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_protection_zones_application_users_user_id",
                table: "protection_zones",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_protection_zones_application_users_user_modified_id",
                table: "protection_zones",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_quad75s_districts_districts_district_id",
                schema: "public",
                table: "quad75s_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_quad75s_districts_quad75s_quad75_id",
                schema: "public",
                table: "quad75s_districts",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_regional_plant_lists_plant_species_plant_species_id",
                table: "regional_plant_lists",
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
                name: "fk_site_calling_detections_application_users_user_id",
                table: "site_calling_detections",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_application_users_user_modified_id",
                table: "site_calling_detections",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_bird_species_bird_species_found_id",
                table: "site_calling_detections",
                column: "bird_species_found_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_districts_district_id",
                table: "site_calling_detections",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_hex160s_hex160_id",
                table: "site_calling_detections",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_hex500s_hex500_id",
                table: "site_calling_detections",
                column: "hex500_id",
                principalTable: "hex500s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_detections_quad75s_quad75_id",
                table: "site_calling_detections",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

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
                name: "fk_site_calling_detections_watersheds_watershed_id",
                table: "site_calling_detections",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks",
                column: "guid",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_application_users_user_id",
                table: "site_callings",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_application_users_user_modified_id",
                table: "site_callings",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_bird_species_bird_species_survey_id",
                table: "site_callings",
                column: "bird_species_survey_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_districts_district_id",
                table: "site_callings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_hex160s_hex160_id",
                table: "site_callings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_hex500s_hex500_id",
                table: "site_callings",
                column: "hex500_id",
                principalTable: "hex500s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_protection_zones_protection_zone_id",
                table: "site_callings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_quad75s_quad75_id",
                table: "site_callings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_site_callings_watersheds_watershed_id",
                table: "site_callings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_ggows_districts_district_id",
                table: "spi_ggows",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_ggows_watersheds_watershed_id",
                table: "spi_ggows",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_nogos_districts_district_id",
                table: "spi_nogos",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_nogos_watersheds_watershed_id",
                table: "spi_nogos",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_points_districts_district_id",
                table: "spi_plant_points",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_points_plant_species_plant_species_id",
                table: "spi_plant_points",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_plant_species_plant_species_id",
                table: "spi_plant_polygons",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_hex160s_spi_plant_polygons_spi_plant_pol",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                column: "spi_plant_polygon_id",
                principalTable: "spi_plant_polygons",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_quad75s_spi_plant_polygons_spi_plant_pol",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                column: "spi_plant_polygon_id",
                principalTable: "spi_plant_polygons",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_watersheds_spi_plant_polygons_spi_plant_",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                column: "spi_plant_polygon_id",
                principalTable: "spi_plant_polygons",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_plant_polygons_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_spi_spows_districts_district_id",
                table: "spi_spows",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_spows_watersheds_watershed_id",
                table: "spi_spows",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_wildlife_sightings_districts_district_id",
                table: "spi_wildlife_sightings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_spi_wildlife_sightings_watersheds_watershed_id",
                table: "spi_wildlife_sightings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_template_fields_templates_template_id",
                schema: "flex",
                table: "template_fields",
                column: "template_id",
                principalSchema: "flex",
                principalTable: "templates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_flex_records_application_users_user_id",
                table: "user_flex_records",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_user_flex_records_application_users_user_modified_id",
                table: "user_flex_records",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_user_flex_records_data_forms_data_form_id",
                table: "user_flex_records",
                column: "data_form_id",
                principalSchema: "flex",
                principalTable: "data_forms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_map_layers_application_users_application_user_id",
                table: "user_map_layers",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "fk_users_districts_application_users_application_user_id",
                schema: "public",
                table: "users_districts",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_districts_districts_district_id",
                schema: "public",
                table: "users_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_watersheds_districts_districts_district_id",
                schema: "public",
                table: "watersheds_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_watersheds_districts_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_districts",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_watersheds_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_active_botanical_survey_areas_application_users_application",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_active_botanical_survey_areas_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_active_hex160s_application_users_application_user_id",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_active_hex160s_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_amphibian_surveys_amphibian_survey_temp_",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_application_users_user_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_elements_application_users_user_modified_id",
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
                name: "fk_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_locations_found_amphibian_species_amphibian_speci",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_points_of_interest_amphibian_species_other_wildli",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_application_users_user_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_application_users_user_modified_id",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_districts_district_temp_id1",
                table: "amphibian_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_hex160s_amphibian_surveys_amphibian_surve",
                schema: "public",
                table: "amphibian_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_quad75s_amphibian_surveys_amphibian_surve",
                schema: "public",
                table: "amphibian_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_watersheds_amphibian_surveys_amphibian_su",
                schema: "public",
                table: "amphibian_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_amphibian_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_application_users_application_groups_application_group_id",
                table: "application_users");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_application_users_user_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_application_users_user_modified_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_botanical_scopings_botanical_scoping_tem",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_elements_botanical_survey_areas_botanical_survey_",
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
                name: "fk_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_plants_list_botanical_plants_of_interest_botanica",
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
                name: "fk_botanical_scoping_species_application_users_user_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scoping_species_application_users_user_modified_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scoping_species_botanical_scopings_botanical_scop",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scoping_species_plant_species_plant_species_temp_",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_application_users_user_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_application_users_user_modified_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_regions_region_temp_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_thp_areas_thp_area_temp_id",
                table: "botanical_scopings");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_districts_botanical_scopings_botanical_s",
                schema: "public",
                table: "botanical_scopings_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_districts_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_quad75s_botanical_scopings_botanical_sco",
                schema: "public",
                table: "botanical_scopings_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_watersheds_botanical_scopings_botanical_",
                schema: "public",
                table: "botanical_scopings_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_scopings_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_application_users_user_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_application_users_user_modified_id",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_botanical_scopings_botanical_scoping",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_districts_district_temp_id3",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_thp_areas_thp_area_temp_id1",
                table: "botanical_survey_areas");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_hex160s_botanical_survey_areas_botan",
                schema: "public",
                table: "botanical_survey_areas_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_quad75s_botanical_survey_areas_botan",
                schema: "public",
                table: "botanical_survey_areas_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_watersheds_botanical_survey_areas_bo",
                schema: "public",
                table: "botanical_survey_areas_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_survey_areas_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_application_users_user_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_application_users_user_modified_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_botanical_survey_areas_botanical_survey_a",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_districts_district_temp_id4",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_thp_areas_thp_area_temp_id2",
                table: "botanical_surveys");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_hex160s_botanical_surveys_botanical_surve",
                schema: "public",
                table: "botanical_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_quad75s_botanical_surveys_botanical_surve",
                schema: "public",
                table: "botanical_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_watersheds_botanical_surveys_botanical_su",
                schema: "public",
                table: "botanical_surveys_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_botanical_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds");

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
                name: "fk_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_plant_species_plant_species_temp_id3",
                table: "cnddb_occurrences");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_districts_cnddb_occurrences_cnddb_occurre",
                schema: "public",
                table: "cnddb_occurrences_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_districts_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_hex160s_cnddb_occurrences_cnddb_occurrenc",
                schema: "public",
                table: "cnddb_occurrences_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_quad75s_cnddb_occurrences_cnddb_occurrenc",
                schema: "public",
                table: "cnddb_occurrences_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_watersheds_cnddb_occurrences_cnddb_occurr",
                schema: "public",
                table: "cnddb_occurrences_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_occurrences_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_plant_species_plant_species_temp_id4",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_quad75s_quad75temp_id3",
                table: "cnddb_quad_elements");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_districts_cnddb_quad_elements_cnddb_qua",
                schema: "public",
                table: "cnddb_quad_elements_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_cnddb_quad_elements_districts_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_data_form_fields_data_forms_data_form_id",
                schema: "flex",
                table: "data_form_fields");

            migrationBuilder.DropForeignKey(
                name: "fk_data_form_fields_template_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields");

            migrationBuilder.DropForeignKey(
                name: "fk_data_forms_templates_template_id",
                schema: "flex",
                table: "data_forms");

            migrationBuilder.DropForeignKey(
                name: "fk_device_infos_amphibian_elements_amphibian_element_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_device_infos_amphibian_surveys_amphibian_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_device_infos_botanical_elements_botanical_element_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_device_infos_botanical_surveys_botanical_survey_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_device_infos_site_callings_site_calling_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_district_extended_geometries_districts_guid",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "fk_flowering_timelines_plant_species_plant_species_temp_id5",
                table: "flowering_timelines");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160_required_passes_application_users_user_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160_required_passes_application_users_user_modified_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160_required_passes_bird_species_bird_species_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160_required_passes_hex160s_hex160_id",
                table: "hex160_required_passes");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_protection_zones_current_protection_zone_id",
                table: "hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_districts_districts_district_id",
                schema: "public",
                table: "hex160s_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_districts_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_protection_zones_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_protection_zones_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_quad75s_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_watersheds_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_hex160s_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_other_wildlife_records_site_callings_site_calling_temp_id1",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "fk_other_wildlife_records_wildlife_species_wildlife_species_te",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_application_users_user_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_application_users_user_modified_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_bird_species_bird_species_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_districts_district_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_protection_zones_protection_zone_temp_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_quad75s_quad75temp_id4",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "fk_permanent_call_stations_application_users_user_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "fk_permanent_call_stations_application_users_user_modified_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "fk_permanent_call_stations_hex160s_hex160_id",
                table: "permanent_call_stations");

            migrationBuilder.DropForeignKey(
                name: "fk_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "fk_pictures_botanical_elements_botanical_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "fk_pictures_owl_bandings_owl_banding_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "fk_pictures_site_callings_site_calling_temp_id2",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "fk_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "fk_plant_protection_summaries_plant_species_plant_species_temp",
                table: "plant_protection_summaries");

            migrationBuilder.DropForeignKey(
                name: "fk_protection_zones_application_users_user_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "fk_protection_zones_application_users_user_modified_id",
                table: "protection_zones");

            migrationBuilder.DropForeignKey(
                name: "fk_quad75s_districts_districts_district_id",
                schema: "public",
                table: "quad75s_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_quad75s_districts_quad75s_quad75_id",
                schema: "public",
                table: "quad75s_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_regional_plant_lists_plant_species_plant_species_id",
                table: "regional_plant_lists");

            migrationBuilder.DropForeignKey(
                name: "fk_regional_plant_lists_regions_region_temp_id1",
                table: "regional_plant_lists");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_application_users_user_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_application_users_user_modified_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_bird_species_bird_species_found_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_districts_district_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_hex160s_hex160_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_hex500s_hex500_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_quad75s_quad75_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_site_callings_site_calling_temp_id3",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_user_locations_user_location_temp_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_detections_watersheds_watershed_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "fk_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_application_users_user_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_application_users_user_modified_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_bird_species_bird_species_survey_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_districts_district_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_hex160s_hex160_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_hex500s_hex500_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_protection_zones_protection_zone_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_quad75s_quad75_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_site_callings_watersheds_watershed_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_ggows_districts_district_id",
                table: "spi_ggows");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_ggows_watersheds_watershed_id",
                table: "spi_ggows");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_nogos_districts_district_id",
                table: "spi_nogos");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_nogos_watersheds_watershed_id",
                table: "spi_nogos");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_points_districts_district_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_points_plant_species_plant_species_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_plant_species_plant_species_id",
                table: "spi_plant_polygons");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_hex160s_spi_plant_polygons_spi_plant_pol",
                schema: "public",
                table: "spi_plant_polygons_hex160s");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_quad75s_spi_plant_polygons_spi_plant_pol",
                schema: "public",
                table: "spi_plant_polygons_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_watersheds_spi_plant_polygons_spi_plant_",
                schema: "public",
                table: "spi_plant_polygons_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_plant_polygons_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_spows_districts_district_id",
                table: "spi_spows");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_spows_watersheds_watershed_id",
                table: "spi_spows");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_wildlife_sightings_districts_district_id",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropForeignKey(
                name: "fk_spi_wildlife_sightings_watersheds_watershed_id",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropForeignKey(
                name: "fk_template_fields_templates_template_id",
                schema: "flex",
                table: "template_fields");

            migrationBuilder.DropForeignKey(
                name: "fk_user_flex_records_application_users_user_id",
                table: "user_flex_records");

            migrationBuilder.DropForeignKey(
                name: "fk_user_flex_records_application_users_user_modified_id",
                table: "user_flex_records");

            migrationBuilder.DropForeignKey(
                name: "fk_user_flex_records_data_forms_data_form_id",
                table: "user_flex_records");

            migrationBuilder.DropForeignKey(
                name: "fk_user_map_layers_application_users_application_user_id",
                table: "user_map_layers");

            migrationBuilder.DropForeignKey(
                name: "fk_users_districts_application_users_application_user_id",
                schema: "public",
                table: "users_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_users_districts_districts_district_id",
                schema: "public",
                table: "users_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_watersheds_districts_districts_district_id",
                schema: "public",
                table: "watersheds_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_watersheds_districts_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_districts");

            migrationBuilder.DropForeignKey(
                name: "fk_watersheds_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropForeignKey(
                name: "fk_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_wildlife_species",
                table: "wildlife_species");

            migrationBuilder.DropPrimaryKey(
                name: "pk_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_watersheds_districts",
                schema: "public",
                table: "watersheds_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_watersheds",
                table: "watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users_districts",
                schema: "public",
                table: "users_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_map_layers",
                table: "user_map_layers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_locations",
                table: "user_locations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_flex_records",
                table: "user_flex_records");

            migrationBuilder.DropPrimaryKey(
                name: "pk_thp_areas",
                table: "thp_areas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_templates",
                schema: "flex",
                table: "templates");

            migrationBuilder.DropPrimaryKey(
                name: "pk_template_fields",
                schema: "flex",
                table: "template_fields");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_wildlife_sightings",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_spows",
                table: "spi_spows");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_plant_polygons_watersheds",
                schema: "public",
                table: "spi_plant_polygons_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_plant_polygons_quad75s",
                schema: "public",
                table: "spi_plant_polygons_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_plant_polygons_hex160s",
                schema: "public",
                table: "spi_plant_polygons_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_plant_polygons",
                table: "spi_plant_polygons");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_plant_points",
                table: "spi_plant_points");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_nogos",
                table: "spi_nogos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_spi_ggows",
                table: "spi_ggows");

            migrationBuilder.DropPrimaryKey(
                name: "pk_site_callings",
                table: "site_callings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_site_calling_tracks",
                table: "site_calling_tracks");

            migrationBuilder.DropPrimaryKey(
                name: "pk_site_calling_detections",
                table: "site_calling_detections");

            migrationBuilder.DropPrimaryKey(
                name: "pk_regions",
                table: "regions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_regional_plant_lists",
                table: "regional_plant_lists");

            migrationBuilder.DropPrimaryKey(
                name: "pk_quad75s_districts",
                schema: "public",
                table: "quad75s_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_quad75s",
                table: "quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_protection_zones",
                table: "protection_zones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_plant_species",
                table: "plant_species");

            migrationBuilder.DropPrimaryKey(
                name: "pk_plant_protection_summaries",
                table: "plant_protection_summaries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pictures",
                table: "pictures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_permanent_call_stations",
                table: "permanent_call_stations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_owl_bandings",
                table: "owl_bandings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_other_wildlife_records",
                table: "other_wildlife_records");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex500s",
                table: "hex500s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex160s_watersheds",
                schema: "public",
                table: "hex160s_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex160s_quad75s",
                schema: "public",
                table: "hex160s_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex160s_protection_zones",
                schema: "public",
                table: "hex160s_protection_zones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex160s_districts",
                schema: "public",
                table: "hex160s_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex160s",
                table: "hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hex160_required_passes",
                table: "hex160_required_passes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_flowering_timelines",
                table: "flowering_timelines");

            migrationBuilder.DropPrimaryKey(
                name: "pk_districts",
                table: "districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_district_extended_geometries",
                table: "district_extended_geometries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_device_infos",
                table: "device_infos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_deleted_geometries",
                table: "deleted_geometries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_data_forms",
                schema: "flex",
                table: "data_forms");

            migrationBuilder.DropPrimaryKey(
                name: "pk_data_form_fields",
                schema: "flex",
                table: "data_form_fields");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_quad_elements_districts",
                schema: "public",
                table: "cnddb_quad_elements_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_quad_elements",
                table: "cnddb_quad_elements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_occurrences_watersheds",
                schema: "public",
                table: "cnddb_occurrences_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_occurrences_quad75s",
                schema: "public",
                table: "cnddb_occurrences_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_occurrences_hex160s",
                schema: "public",
                table: "cnddb_occurrences_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_occurrences_districts",
                schema: "public",
                table: "cnddb_occurrences_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnddb_occurrences",
                table: "cnddb_occurrences");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cdfw_spotted_owls",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cdfw_spotted_owl_diagrams",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_surveys_watersheds",
                schema: "public",
                table: "botanical_surveys_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_surveys_quad75s",
                schema: "public",
                table: "botanical_surveys_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_surveys_hex160s",
                schema: "public",
                table: "botanical_surveys_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_surveys",
                table: "botanical_surveys");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_survey_areas_watersheds",
                schema: "public",
                table: "botanical_survey_areas_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_survey_areas_quad75s",
                schema: "public",
                table: "botanical_survey_areas_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_survey_areas_hex160s",
                schema: "public",
                table: "botanical_survey_areas_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_survey_areas",
                table: "botanical_survey_areas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_scopings_watersheds",
                schema: "public",
                table: "botanical_scopings_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_scopings_quad75s",
                schema: "public",
                table: "botanical_scopings_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_scopings_districts",
                schema: "public",
                table: "botanical_scopings_districts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_scopings",
                table: "botanical_scopings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_scoping_species",
                table: "botanical_scoping_species");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_points_of_interest",
                table: "botanical_points_of_interest");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_plants_of_interest",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_plants_list",
                table: "botanical_plants_list");

            migrationBuilder.DropPrimaryKey(
                name: "pk_botanical_elements",
                table: "botanical_elements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_bird_species",
                table: "bird_species");

            migrationBuilder.DropPrimaryKey(
                name: "pk_application_users",
                table: "application_users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_application_groups",
                table: "application_groups");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_surveys_watersheds",
                schema: "public",
                table: "amphibian_surveys_watersheds");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_surveys_quad75s",
                schema: "public",
                table: "amphibian_surveys_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_surveys_hex160s",
                schema: "public",
                table: "amphibian_surveys_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_surveys",
                table: "amphibian_surveys");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_species",
                table: "amphibian_species");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_points_of_interest",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_locations_found",
                table: "amphibian_locations_found");

            migrationBuilder.DropPrimaryKey(
                name: "pk_amphibian_elements",
                table: "amphibian_elements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_active_hex160s",
                schema: "public",
                table: "active_hex160s");

            migrationBuilder.DropPrimaryKey(
                name: "pk_active_botanical_survey_areas",
                schema: "public",
                table: "active_botanical_survey_areas");

            migrationBuilder.RenameIndex(
                name: "ix_watersheds_quad75s_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                newName: "IX_watersheds_quad75s_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_watersheds_districts_watershed_id",
                schema: "public",
                table: "watersheds_districts",
                newName: "IX_watersheds_districts_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_districts_district_id",
                schema: "public",
                table: "users_districts",
                newName: "IX_users_districts_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_map_layers_application_user_id",
                table: "user_map_layers",
                newName: "IX_user_map_layers_application_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_flex_records_user_modified_id",
                table: "user_flex_records",
                newName: "IX_user_flex_records_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_flex_records_user_id",
                table: "user_flex_records",
                newName: "IX_user_flex_records_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_flex_records_data_form_id",
                table: "user_flex_records",
                newName: "IX_user_flex_records_data_form_id");

            migrationBuilder.RenameIndex(
                name: "ix_template_fields_template_id",
                schema: "flex",
                table: "template_fields",
                newName: "IX_template_fields_template_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_wildlife_sightings_watershed_id",
                table: "spi_wildlife_sightings",
                newName: "IX_spi_wildlife_sightings_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_wildlife_sightings_district_id",
                table: "spi_wildlife_sightings",
                newName: "IX_spi_wildlife_sightings_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_spows_watershed_id",
                table: "spi_spows",
                newName: "IX_spi_spows_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_spows_district_id",
                table: "spi_spows",
                newName: "IX_spi_spows_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_polygons_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                newName: "IX_spi_plant_polygons_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_polygons_quad75s_spi_plant_polygon_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                newName: "IX_spi_plant_polygons_quad75s_spi_plant_polygon_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_polygons_hex160s_spi_plant_polygon_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                newName: "IX_spi_plant_polygons_hex160s_spi_plant_polygon_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_polygons_plant_species_id",
                table: "spi_plant_polygons",
                newName: "IX_spi_plant_polygons_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_polygons_district_id",
                table: "spi_plant_polygons",
                newName: "IX_spi_plant_polygons_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_points_watershed_id",
                table: "spi_plant_points",
                newName: "IX_spi_plant_points_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_points_quad75_id",
                table: "spi_plant_points",
                newName: "IX_spi_plant_points_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_points_plant_species_id",
                table: "spi_plant_points",
                newName: "IX_spi_plant_points_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_points_hex160_id",
                table: "spi_plant_points",
                newName: "IX_spi_plant_points_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_plant_points_district_id",
                table: "spi_plant_points",
                newName: "IX_spi_plant_points_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_nogos_watershed_id",
                table: "spi_nogos",
                newName: "IX_spi_nogos_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_nogos_district_id",
                table: "spi_nogos",
                newName: "IX_spi_nogos_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_ggows_watershed_id",
                table: "spi_ggows",
                newName: "IX_spi_ggows_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_spi_ggows_district_id",
                table: "spi_ggows",
                newName: "IX_spi_ggows_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_watershed_id",
                table: "site_callings",
                newName: "IX_site_callings_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_user_modified_id",
                table: "site_callings",
                newName: "IX_site_callings_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_user_id",
                table: "site_callings",
                newName: "IX_site_callings_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_quad75_id",
                table: "site_callings",
                newName: "IX_site_callings_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_protection_zone_id",
                table: "site_callings",
                newName: "IX_site_callings_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_hex500_id",
                table: "site_callings",
                newName: "IX_site_callings_hex500_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_hex160_id",
                table: "site_callings",
                newName: "IX_site_callings_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_district_id",
                table: "site_callings",
                newName: "IX_site_callings_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_callings_bird_species_survey_id",
                table: "site_callings",
                newName: "IX_site_callings_bird_species_survey_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_watershed_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_user_modified_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_user_location_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_user_location_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_user_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_site_calling_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_quad75_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_hex500_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_hex500_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_hex160_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_district_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_site_calling_detections_bird_species_found_id",
                table: "site_calling_detections",
                newName: "IX_site_calling_detections_bird_species_found_id");

            migrationBuilder.RenameIndex(
                name: "ix_regional_plant_lists_region_id",
                table: "regional_plant_lists",
                newName: "IX_regional_plant_lists_region_id");

            migrationBuilder.RenameIndex(
                name: "ix_regional_plant_lists_plant_species_id",
                table: "regional_plant_lists",
                newName: "IX_regional_plant_lists_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_quad75s_districts_quad75_id",
                schema: "public",
                table: "quad75s_districts",
                newName: "IX_quad75s_districts_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_protection_zones_user_modified_id",
                table: "protection_zones",
                newName: "IX_protection_zones_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_protection_zones_user_id",
                table: "protection_zones",
                newName: "IX_protection_zones_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_plant_protection_summaries_plant_species_id",
                table: "plant_protection_summaries",
                newName: "IX_plant_protection_summaries_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_plant_protection_summaries_district_id",
                table: "plant_protection_summaries",
                newName: "IX_plant_protection_summaries_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_pictures_site_calling_id",
                table: "pictures",
                newName: "IX_pictures_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "ix_pictures_owl_banding_id",
                table: "pictures",
                newName: "IX_pictures_owl_banding_id");

            migrationBuilder.RenameIndex(
                name: "ix_pictures_botanical_element_id",
                table: "pictures",
                newName: "IX_pictures_botanical_element_id");

            migrationBuilder.RenameIndex(
                name: "ix_pictures_amphibian_element_id",
                table: "pictures",
                newName: "IX_pictures_amphibian_element_id");

            migrationBuilder.RenameIndex(
                name: "ix_permanent_call_stations_user_modified_id",
                table: "permanent_call_stations",
                newName: "IX_permanent_call_stations_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_permanent_call_stations_user_id",
                table: "permanent_call_stations",
                newName: "IX_permanent_call_stations_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_permanent_call_stations_hex160_id",
                table: "permanent_call_stations",
                newName: "IX_permanent_call_stations_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_watershed_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_user_modified_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_user_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_quad75_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_protection_zone_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_hex160_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_district_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_owl_bandings_bird_species_id",
                table: "owl_bandings",
                newName: "IX_owl_bandings_bird_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_other_wildlife_records_wildlife_species_id",
                table: "other_wildlife_records",
                newName: "IX_other_wildlife_records_wildlife_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_other_wildlife_records_site_calling_id",
                table: "other_wildlife_records",
                newName: "IX_other_wildlife_records_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160s_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds",
                newName: "IX_hex160s_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s",
                newName: "IX_hex160s_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160s_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                newName: "IX_hex160s_protection_zones_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160s_districts_hex160_id",
                schema: "public",
                table: "hex160s_districts",
                newName: "IX_hex160s_districts_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160s_current_protection_zone_id",
                table: "hex160s",
                newName: "IX_hex160s_current_protection_zone_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160_required_passes_user_modified_id",
                table: "hex160_required_passes",
                newName: "IX_hex160_required_passes_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160_required_passes_user_id",
                table: "hex160_required_passes",
                newName: "IX_hex160_required_passes_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160_required_passes_hex160_id",
                table: "hex160_required_passes",
                newName: "IX_hex160_required_passes_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_hex160_required_passes_bird_species_id",
                table: "hex160_required_passes",
                newName: "IX_hex160_required_passes_bird_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_flowering_timelines_plant_species_id",
                table: "flowering_timelines",
                newName: "IX_flowering_timelines_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_device_infos_site_calling_id",
                table: "device_infos",
                newName: "IX_device_infos_site_calling_id");

            migrationBuilder.RenameIndex(
                name: "ix_device_infos_owl_banding_id",
                table: "device_infos",
                newName: "IX_device_infos_owl_banding_id");

            migrationBuilder.RenameIndex(
                name: "ix_device_infos_botanical_survey_id",
                table: "device_infos",
                newName: "IX_device_infos_botanical_survey_id");

            migrationBuilder.RenameIndex(
                name: "ix_device_infos_botanical_element_id",
                table: "device_infos",
                newName: "IX_device_infos_botanical_element_id");

            migrationBuilder.RenameIndex(
                name: "ix_device_infos_amphibian_survey_id",
                table: "device_infos",
                newName: "IX_device_infos_amphibian_survey_id");

            migrationBuilder.RenameIndex(
                name: "ix_device_infos_amphibian_element_id",
                table: "device_infos",
                newName: "IX_device_infos_amphibian_element_id");

            migrationBuilder.RenameIndex(
                name: "ix_data_forms_template_id",
                schema: "flex",
                table: "data_forms",
                newName: "IX_data_forms_template_id");

            migrationBuilder.RenameIndex(
                name: "ix_data_form_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields",
                newName: "IX_data_form_fields_template_field_id");

            migrationBuilder.RenameIndex(
                name: "ix_data_form_fields_data_form_id",
                schema: "flex",
                table: "data_form_fields",
                newName: "IX_data_form_fields_data_form_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_quad_elements_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                newName: "IX_cnddb_quad_elements_districts_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_quad_elements_quad75_id",
                table: "cnddb_quad_elements",
                newName: "IX_cnddb_quad_elements_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_quad_elements_plant_species_id",
                table: "cnddb_quad_elements",
                newName: "IX_cnddb_quad_elements_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_occurrences_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                newName: "IX_cnddb_occurrences_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_occurrences_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                newName: "IX_cnddb_occurrences_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_occurrences_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                newName: "IX_cnddb_occurrences_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_occurrences_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts",
                newName: "IX_cnddb_occurrences_districts_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_cnddb_occurrences_plant_species_id",
                table: "cnddb_occurrences",
                newName: "IX_cnddb_occurrences_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_cdfw_spotted_owls_watershed_id",
                table: "cdfw_spotted_owls",
                newName: "IX_cdfw_spotted_owls_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_cdfw_spotted_owls_quad75_id",
                table: "cdfw_spotted_owls",
                newName: "IX_cdfw_spotted_owls_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_cdfw_spotted_owls_hex160_id",
                table: "cdfw_spotted_owls",
                newName: "IX_cdfw_spotted_owls_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_cdfw_spotted_owls_district_id",
                table: "cdfw_spotted_owls",
                newName: "IX_cdfw_spotted_owls_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_cdfw_spotted_owl_diagrams_district_id",
                table: "cdfw_spotted_owl_diagrams",
                newName: "IX_cdfw_spotted_owl_diagrams_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds",
                newName: "IX_botanical_surveys_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s",
                newName: "IX_botanical_surveys_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s",
                newName: "IX_botanical_surveys_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_user_modified_id",
                table: "botanical_surveys",
                newName: "IX_botanical_surveys_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_user_id",
                table: "botanical_surveys",
                newName: "IX_botanical_surveys_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_thp_area_id",
                table: "botanical_surveys",
                newName: "IX_botanical_surveys_thp_area_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_district_id",
                table: "botanical_surveys",
                newName: "IX_botanical_surveys_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_botanical_survey_area_id",
                table: "botanical_surveys",
                newName: "IX_botanical_surveys_botanical_survey_area_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_surveys_botanical_scoping_id",
                table: "botanical_surveys",
                newName: "IX_botanical_surveys_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                newName: "IX_botanical_survey_areas_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                newName: "IX_botanical_survey_areas_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                newName: "IX_botanical_survey_areas_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_user_modified_id",
                table: "botanical_survey_areas",
                newName: "IX_botanical_survey_areas_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_user_id",
                table: "botanical_survey_areas",
                newName: "IX_botanical_survey_areas_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_thp_area_id",
                table: "botanical_survey_areas",
                newName: "IX_botanical_survey_areas_thp_area_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_district_id",
                table: "botanical_survey_areas",
                newName: "IX_botanical_survey_areas_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_survey_areas_botanical_scoping_id",
                table: "botanical_survey_areas",
                newName: "IX_botanical_survey_areas_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds",
                newName: "IX_botanical_scopings_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s",
                newName: "IX_botanical_scopings_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts",
                newName: "IX_botanical_scopings_districts_district_id");

            migrationBuilder.RenameColumn(
                name: "forester",
                table: "botanical_scopings",
                newName: "Forester");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_user_modified_id",
                table: "botanical_scopings",
                newName: "IX_botanical_scopings_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_user_id",
                table: "botanical_scopings",
                newName: "IX_botanical_scopings_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_thp_area_id",
                table: "botanical_scopings",
                newName: "IX_botanical_scopings_thp_area_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scopings_region_id",
                table: "botanical_scopings",
                newName: "IX_botanical_scopings_region_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scoping_species_user_modified_id",
                table: "botanical_scoping_species",
                newName: "IX_botanical_scoping_species_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scoping_species_user_id",
                table: "botanical_scoping_species",
                newName: "IX_botanical_scoping_species_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scoping_species_plant_species_id",
                table: "botanical_scoping_species",
                newName: "IX_botanical_scoping_species_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_scoping_species_botanical_scoping_id",
                table: "botanical_scoping_species",
                newName: "IX_botanical_scoping_species_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_plants_of_interest_plant_species_id",
                table: "botanical_plants_of_interest",
                newName: "IX_botanical_plants_of_interest_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_plants_list_plant_species_id",
                table: "botanical_plants_list",
                newName: "IX_botanical_plants_list_plant_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_plants_list_botanical_plant_of_interest_id",
                table: "botanical_plants_list",
                newName: "IX_botanical_plants_list_botanical_plant_of_interest_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_watershed_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_user_modified_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_user_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_quad75_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_hex160_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_district_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_botanical_survey_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_botanical_survey_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_botanical_survey_area_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_botanical_survey_area_id");

            migrationBuilder.RenameIndex(
                name: "ix_botanical_elements_botanical_scoping_id",
                table: "botanical_elements",
                newName: "IX_botanical_elements_botanical_scoping_id");

            migrationBuilder.RenameIndex(
                name: "ix_application_users_application_group_id",
                table: "application_users",
                newName: "IX_application_users_application_group_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_surveys_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                newName: "IX_amphibian_surveys_watersheds_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_surveys_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                newName: "IX_amphibian_surveys_quad75s_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_surveys_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                newName: "IX_amphibian_surveys_hex160s_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_surveys_user_modified_id",
                table: "amphibian_surveys",
                newName: "IX_amphibian_surveys_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_surveys_user_id",
                table: "amphibian_surveys",
                newName: "IX_amphibian_surveys_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_surveys_district_id",
                table: "amphibian_surveys",
                newName: "IX_amphibian_surveys_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_points_of_interest_other_wildlife_id",
                table: "amphibian_points_of_interest",
                newName: "IX_amphibian_points_of_interest_other_wildlife_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_locations_found_amphibian_species_id",
                table: "amphibian_locations_found",
                newName: "IX_amphibian_locations_found_amphibian_species_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_watershed_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_watershed_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_user_modified_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_user_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_quad75_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_quad75_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_hex160_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_hex160_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_district_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_district_id");

            migrationBuilder.RenameIndex(
                name: "ix_amphibian_elements_amphibian_survey_id",
                table: "amphibian_elements",
                newName: "IX_amphibian_elements_amphibian_survey_id");

            migrationBuilder.RenameIndex(
                name: "ix_active_hex160s_unit_id",
                schema: "public",
                table: "active_hex160s",
                newName: "IX_active_hex160s_unit_id");

            migrationBuilder.RenameIndex(
                name: "ix_active_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                newName: "IX_active_botanical_survey_areas_unit_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wildlife_species",
                table: "wildlife_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s",
                columns: new[] { "quad75_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_watersheds_districts",
                schema: "public",
                table: "watersheds_districts",
                columns: new[] { "district_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_watersheds",
                table: "watersheds",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users_districts",
                schema: "public",
                table: "users_districts",
                columns: new[] { "application_user_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_map_layers",
                table: "user_map_layers",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_locations",
                table: "user_locations",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_flex_records",
                table: "user_flex_records",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_thp_areas",
                table: "thp_areas",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_templates",
                schema: "flex",
                table: "templates",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_template_fields",
                schema: "flex",
                table: "template_fields",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_wildlife_sightings",
                table: "spi_wildlife_sightings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_spows",
                table: "spi_spows",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_plant_polygons_watersheds",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                columns: new[] { "spi_plant_polygon_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_plant_polygons_quad75s",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                columns: new[] { "quad75_id", "spi_plant_polygon_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_plant_polygons_hex160s",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                columns: new[] { "hex160_id", "spi_plant_polygon_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_plant_polygons",
                table: "spi_plant_polygons",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_plant_points",
                table: "spi_plant_points",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_nogos",
                table: "spi_nogos",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spi_ggows",
                table: "spi_ggows",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_site_callings",
                table: "site_callings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_site_calling_tracks",
                table: "site_calling_tracks",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_site_calling_detections",
                table: "site_calling_detections",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regions",
                table: "regions",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regional_plant_lists",
                table: "regional_plant_lists",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quad75s_districts",
                schema: "public",
                table: "quad75s_districts",
                columns: new[] { "district_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_quad75s",
                table: "quad75s",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_protection_zones",
                table: "protection_zones",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plant_species",
                table: "plant_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plant_protection_summaries",
                table: "plant_protection_summaries",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pictures",
                table: "pictures",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permanent_call_stations",
                table: "permanent_call_stations",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_owl_bandings",
                table: "owl_bandings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_other_wildlife_records",
                table: "other_wildlife_records",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex500s",
                table: "hex500s",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex160s_watersheds",
                schema: "public",
                table: "hex160s_watersheds",
                columns: new[] { "hex160_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex160s_quad75s",
                schema: "public",
                table: "hex160s_quad75s",
                columns: new[] { "hex160_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex160s_protection_zones",
                schema: "public",
                table: "hex160s_protection_zones",
                columns: new[] { "hex160_id", "protection_zone_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex160s_districts",
                schema: "public",
                table: "hex160s_districts",
                columns: new[] { "district_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex160s",
                table: "hex160s",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hex160_required_passes",
                table: "hex160_required_passes",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_flowering_timelines",
                table: "flowering_timelines",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_districts",
                table: "districts",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_district_extended_geometries",
                table: "district_extended_geometries",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_device_infos",
                table: "device_infos",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deleted_geometries",
                table: "deleted_geometries",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_forms",
                schema: "flex",
                table: "data_forms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_form_fields",
                schema: "flex",
                table: "data_form_fields",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_quad_elements_districts",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                columns: new[] { "cnddb_quad_element_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_quad_elements",
                table: "cnddb_quad_elements",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_occurrences_watersheds",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                columns: new[] { "cnddb_occurrence_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_occurrences_quad75s",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                columns: new[] { "cnddb_occurrence_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_occurrences_hex160s",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                columns: new[] { "cnddb_occurrence_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_occurrences_districts",
                schema: "public",
                table: "cnddb_occurrences_districts",
                columns: new[] { "cnddb_occurrence_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_cnddb_occurrences",
                table: "cnddb_occurrences",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cdfw_spotted_owls",
                table: "cdfw_spotted_owls",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cdfw_spotted_owl_diagrams",
                table: "cdfw_spotted_owl_diagrams",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_surveys_watersheds",
                schema: "public",
                table: "botanical_surveys_watersheds",
                columns: new[] { "botanical_survey_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_surveys_quad75s",
                schema: "public",
                table: "botanical_surveys_quad75s",
                columns: new[] { "botanical_survey_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_surveys_hex160s",
                schema: "public",
                table: "botanical_surveys_hex160s",
                columns: new[] { "botanical_survey_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_surveys",
                table: "botanical_surveys",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_survey_areas_watersheds",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                columns: new[] { "botanical_survey_area_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_survey_areas_quad75s",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                columns: new[] { "botanical_survey_area_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_survey_areas_hex160s",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                columns: new[] { "botanical_survey_area_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_survey_areas",
                table: "botanical_survey_areas",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_scopings_watersheds",
                schema: "public",
                table: "botanical_scopings_watersheds",
                columns: new[] { "botanical_scoping_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_scopings_quad75s",
                schema: "public",
                table: "botanical_scopings_quad75s",
                columns: new[] { "botanical_scoping_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_scopings_districts",
                schema: "public",
                table: "botanical_scopings_districts",
                columns: new[] { "botanical_scoping_id", "district_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_scopings",
                table: "botanical_scopings",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_scoping_species",
                table: "botanical_scoping_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_points_of_interest",
                table: "botanical_points_of_interest",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_plants_of_interest",
                table: "botanical_plants_of_interest",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_plants_list",
                table: "botanical_plants_list",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_botanical_elements",
                table: "botanical_elements",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bird_species",
                table: "bird_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_application_users",
                table: "application_users",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_application_groups",
                table: "application_groups",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_surveys_watersheds",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                columns: new[] { "amphibian_survey_id", "watershed_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_surveys_quad75s",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                columns: new[] { "amphibian_survey_id", "quad75_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_surveys_hex160s",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                columns: new[] { "amphibian_survey_id", "hex160_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_surveys",
                table: "amphibian_surveys",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_species",
                table: "amphibian_species",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_points_of_interest",
                table: "amphibian_points_of_interest",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_locations_found",
                table: "amphibian_locations_found",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amphibian_elements",
                table: "amphibian_elements",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_active_hex160s",
                schema: "public",
                table: "active_hex160s",
                columns: new[] { "application_user_id", "unit_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_active_botanical_survey_areas",
                schema: "public",
                table: "active_botanical_survey_areas",
                columns: new[] { "application_user_id", "unit_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_active_botanical_survey_areas_application_users_application~",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_active_botanical_survey_areas_botanical_survey_areas_unit_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "unit_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_active_hex160s_application_users_application_user_id",
                schema: "public",
                table: "active_hex160s",
                column: "application_user_id",
                principalTable: "application_users",
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

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");

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
                name: "FK_amphibian_elements_districts_district_id",
                table: "amphibian_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                table: "amphibian_locations_found",
                column: "amphibian_species_id",
                principalTable: "amphibian_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                table: "amphibian_points_of_interest",
                column: "other_wildlife_id",
                principalTable: "amphibian_species",
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
                name: "FK_amphibian_surveys_districts_district_id",
                table: "amphibian_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_hex160s_amphibian_surveys_amphibian_surve~",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_quad75s_amphibian_surveys_amphibian_surve~",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_watersheds_amphibian_surveys_amphibian_su~",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_application_users_application_groups_application_group_id",
                table: "application_users",
                column: "application_group_id",
                principalTable: "application_groups",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_districts_district_id",
                table: "botanical_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_list_botanical_plants_of_interest_botanica~",
                table: "botanical_plants_list",
                column: "botanical_plant_of_interest_id",
                principalTable: "botanical_plants_of_interest",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_list_plant_species_plant_species_id",
                table: "botanical_plants_list",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_of_interest_botanical_elements_guid",
                table: "botanical_plants_of_interest",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_of_interest_plant_species_plant_species_id",
                table: "botanical_plants_of_interest",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_guid",
                table: "botanical_points_of_interest",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_id",
                table: "botanical_scoping_species",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_modified_id",
                table: "botanical_scoping_species",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scoping_species_botanical_scopings_botanical_scop~",
                table: "botanical_scoping_species",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scoping_species_plant_species_plant_species_id",
                table: "botanical_scoping_species",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_botanical_scopings_regions_region_id",
                table: "botanical_scopings",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_thp_areas_thp_area_id",
                table: "botanical_scopings",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_districts_botanical_scopings_botanical_s~",
                schema: "public",
                table: "botanical_scopings_districts",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_districts_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_quad75s_botanical_scopings_botanical_sco~",
                schema: "public",
                table: "botanical_scopings_quad75s",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_watersheds_botanical_scopings_botanical_~",
                schema: "public",
                table: "botanical_scopings_watersheds",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scopings_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                table: "botanical_survey_areas",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_districts_district_id",
                table: "botanical_survey_areas",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                table: "botanical_survey_areas",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_hex160s_botanical_survey_areas_botan~",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_quad75s_botanical_survey_areas_botan~",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_watersheds_botanical_survey_areas_bo~",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_survey_areas_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_botanical_surveys_botanical_scopings_botanical_scoping_id",
                table: "botanical_surveys",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                table: "botanical_surveys",
                column: "botanical_survey_area_id",
                principalTable: "botanical_survey_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_districts_district_id",
                table: "botanical_surveys",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_thp_areas_thp_area_id",
                table: "botanical_surveys",
                column: "thp_area_id",
                principalTable: "thp_areas",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_hex160s_botanical_surveys_botanical_surve~",
                schema: "public",
                table: "botanical_surveys_hex160s",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_quad75s_botanical_surveys_botanical_surve~",
                schema: "public",
                table: "botanical_surveys_quad75s",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_watersheds_botanical_surveys_botanical_su~",
                schema: "public",
                table: "botanical_surveys_watersheds",
                column: "botanical_survey_id",
                principalTable: "botanical_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_surveys_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_plant_species_plant_species_id",
                table: "cnddb_occurrences",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_districts_cnddb_occurrences_cnddb_occurre~",
                schema: "public",
                table: "cnddb_occurrences_districts",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_districts_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_hex160s_cnddb_occurrences_cnddb_occurrenc~",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_quad75s_cnddb_occurrences_cnddb_occurrenc~",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_watersheds_cnddb_occurrences_cnddb_occurr~",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                column: "cnddb_occurrence_id",
                principalTable: "cnddb_occurrences",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_quad_elements_plant_species_plant_species_id",
                table: "cnddb_quad_elements",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_quad_elements_quad75s_quad75_id",
                table: "cnddb_quad_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_quad_elements_districts_cnddb_quad_elements_cnddb_qua~",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                column: "cnddb_quad_element_id",
                principalTable: "cnddb_quad_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_quad_elements_districts_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_form_fields_data_forms_data_form_id",
                schema: "flex",
                table: "data_form_fields",
                column: "data_form_id",
                principalSchema: "flex",
                principalTable: "data_forms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_form_fields_template_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields",
                column: "template_field_id",
                principalSchema: "flex",
                principalTable: "template_fields",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_forms_templates_template_id",
                schema: "flex",
                table: "data_forms",
                column: "template_id",
                principalSchema: "flex",
                principalTable: "templates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_district_extended_geometries_districts_guid",
                table: "district_extended_geometries",
                column: "guid",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flowering_timelines_plant_species_plant_species_id",
                table: "flowering_timelines",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_hex160_required_passes_bird_species_bird_species_id",
                table: "hex160_required_passes",
                column: "bird_species_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160_required_passes_hex160s_hex160_id",
                table: "hex160_required_passes",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_current_protection_zone_id",
                table: "hex160s",
                column: "current_protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_districts_districts_district_id",
                schema: "public",
                table: "hex160s_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_districts_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_districts",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_protection_zones_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_quad75s_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_quad75s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_watersheds_hex160s_hex160_id",
                schema: "public",
                table: "hex160s_watersheds",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hex160s_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_other_wildlife_records_site_callings_site_calling_id",
                table: "other_wildlife_records",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_other_wildlife_records_wildlife_species_wildlife_species_id",
                table: "other_wildlife_records",
                column: "wildlife_species_id",
                principalTable: "wildlife_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_owl_bandings_bird_species_bird_species_id",
                table: "owl_bandings",
                column: "bird_species_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_districts_district_id",
                table: "owl_bandings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_protection_zones_protection_zone_id",
                table: "owl_bandings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings",
                column: "watershed_id",
                principalTable: "watersheds",
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
                name: "FK_permanent_call_stations_hex160s_hex160_id",
                table: "permanent_call_stations",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_botanical_elements_botanical_element_id",
                table: "pictures",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_owl_bandings_owl_banding_id",
                table: "pictures",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_site_callings_site_calling_id",
                table: "pictures",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_plant_protection_summaries_districts_district_id",
                table: "plant_protection_summaries",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_plant_protection_summaries_plant_species_plant_species_id",
                table: "plant_protection_summaries",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_quad75s_districts_districts_district_id",
                schema: "public",
                table: "quad75s_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quad75s_districts_quad75s_quad75_id",
                schema: "public",
                table: "quad75s_districts",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_regional_plant_lists_plant_species_plant_species_id",
                table: "regional_plant_lists",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_regional_plant_lists_regions_region_id",
                table: "regional_plant_lists",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_application_users_user_id",
                table: "site_calling_detections",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_application_users_user_modified_id",
                table: "site_calling_detections",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_bird_species_bird_species_found_id",
                table: "site_calling_detections",
                column: "bird_species_found_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_districts_district_id",
                table: "site_calling_detections",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_hex160s_hex160_id",
                table: "site_calling_detections",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_hex500s_hex500_id",
                table: "site_calling_detections",
                column: "hex500_id",
                principalTable: "hex500s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_quad75s_quad75_id",
                table: "site_calling_detections",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_site_callings_site_calling_id",
                table: "site_calling_detections",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_user_locations_user_location_id",
                table: "site_calling_detections",
                column: "user_location_id",
                principalTable: "user_locations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_watersheds_watershed_id",
                table: "site_calling_detections",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks",
                column: "guid",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_bird_species_bird_species_survey_id",
                table: "site_callings",
                column: "bird_species_survey_id",
                principalTable: "bird_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_districts_district_id",
                table: "site_callings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_hex160s_hex160_id",
                table: "site_callings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_hex500s_hex500_id",
                table: "site_callings",
                column: "hex500_id",
                principalTable: "hex500s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_protection_zones_protection_zone_id",
                table: "site_callings",
                column: "protection_zone_id",
                principalTable: "protection_zones",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_quad75s_quad75_id",
                table: "site_callings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_watersheds_watershed_id",
                table: "site_callings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_ggows_districts_district_id",
                table: "spi_ggows",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_ggows_watersheds_watershed_id",
                table: "spi_ggows",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_nogos_districts_district_id",
                table: "spi_nogos",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_nogos_watersheds_watershed_id",
                table: "spi_nogos",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_districts_district_id",
                table: "spi_plant_points",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_plant_species_plant_species_id",
                table: "spi_plant_points",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_districts_district_id",
                table: "spi_plant_polygons",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_plant_species_plant_species_id",
                table: "spi_plant_polygons",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_hex160s_hex160s_hex160_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_hex160s_spi_plant_polygons_spi_plant_pol~",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                column: "spi_plant_polygon_id",
                principalTable: "spi_plant_polygons",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_quad75s_spi_plant_polygons_spi_plant_pol~",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                column: "spi_plant_polygon_id",
                principalTable: "spi_plant_polygons",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_watersheds_spi_plant_polygons_spi_plant_~",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                column: "spi_plant_polygon_id",
                principalTable: "spi_plant_polygons",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_polygons_watersheds_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_spows_districts_district_id",
                table: "spi_spows",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_spows_watersheds_watershed_id",
                table: "spi_spows",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_wildlife_sightings_districts_district_id",
                table: "spi_wildlife_sightings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_wildlife_sightings_watersheds_watershed_id",
                table: "spi_wildlife_sightings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_template_fields_templates_template_id",
                schema: "flex",
                table: "template_fields",
                column: "template_id",
                principalSchema: "flex",
                principalTable: "templates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_flex_records_application_users_user_id",
                table: "user_flex_records",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_flex_records_application_users_user_modified_id",
                table: "user_flex_records",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_flex_records_data_forms_data_form_id",
                table: "user_flex_records",
                column: "data_form_id",
                principalSchema: "flex",
                principalTable: "data_forms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_map_layers_application_users_application_user_id",
                table: "user_map_layers",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_districts_application_users_application_user_id",
                schema: "public",
                table: "users_districts",
                column: "application_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_districts_districts_district_id",
                schema: "public",
                table: "users_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_districts_districts_district_id",
                schema: "public",
                table: "watersheds_districts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_districts_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_districts",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_quad75s_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
