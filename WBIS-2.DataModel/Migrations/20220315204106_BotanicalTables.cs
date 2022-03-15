using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class BotanicalTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_device_infos_device_info_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_device_infos_device_info_id",
                table: "amphibian_surveys");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_surveys_device_info_id",
                table: "amphibian_surveys");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_elements_device_info_id",
                table: "amphibian_elements");

            migrationBuilder.AddColumn<bool>(
                name: "species_present",
                table: "site_callings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "species_present",
                table: "site_calling_repositories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "amphibian_element_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "amphibian_survey_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_element_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "horizontal_accuracy",
                table: "device_infos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "botanical_points_of_interest",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    record_type = table.Column<string>(type: "text", nullable: true),
                    inundated = table.Column<bool>(type: "boolean", nullable: false),
                    littoral_zone = table.Column<bool>(type: "boolean", nullable: false),
                    herbaceous_vegetation = table.Column<bool>(type: "boolean", nullable: false),
                    woody_vedetation = table.Column<bool>(type: "boolean", nullable: false),
                    instream = table.Column<bool>(type: "boolean", nullable: false),
                    isolated = table.Column<bool>(type: "boolean", nullable: false),
                    rechecks_needed = table.Column<bool>(type: "boolean", nullable: false),
                    recheck = table.Column<bool>(type: "boolean", nullable: false),
                    substrate = table.Column<string>(type: "text", nullable: true),
                    gradient = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    radius = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_points_of_interest", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_points_of_interest_amphibian_elements_amphibian_e~",
                        column: x => x.amphibian_element_id,
                        principalTable: "amphibian_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thp_areas",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    thp_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thp_areas", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "botanical_scopings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    thp_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Forester = table.Column<string>(type: "text", nullable: true),
                    region_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ecological_unit = table.Column<string>(type: "text", nullable: true),
                    elevation_max = table.Column<int>(type: "integer", nullable: false),
                    elevation_min = table.Column<int>(type: "integer", nullable: false),
                    wshd_elevation_max = table.Column<int>(type: "integer", nullable: false),
                    wshd_elevation_min = table.Column<int>(type: "integer", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_scopings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_scopings_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_scopings_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_scopings_thp_areas_thp_area_id",
                        column: x => x.thp_area_id,
                        principalTable: "thp_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_scopings_districts",
                schema: "public",
                columns: table => new
                {
                    botanical_scoping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_scopings_districts", x => new { x.botanical_scoping_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_botanical_scopings_districts_botanical_scopings_botanical_s~",
                        column: x => x.botanical_scoping_id,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_scopings_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_scopings_quad75s",
                schema: "public",
                columns: table => new
                {
                    botanical_scoping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_scopings_quad75s", x => new { x.botanical_scoping_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_botanical_scopings_quad75s_botanical_scopings_botanical_sco~",
                        column: x => x.botanical_scoping_id,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_scopings_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_scopings_watersheds",
                schema: "public",
                columns: table => new
                {
                    botanical_scoping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_scopings_watersheds", x => new { x.botanical_scoping_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_botanical_scopings_watersheds_botanical_scopings_botanical_~",
                        column: x => x.botanical_scoping_id,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_scopings_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_survey_areas",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    thp_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_scoping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_name = table.Column<string>(type: "text", nullable: true),
                    survey_type = table.Column<string>(type: "text", nullable: true),
                    general_habitat = table.Column<string>(type: "text", nullable: true),
                    aspect = table.Column<string>(type: "text", nullable: true),
                    slope = table.Column<string>(type: "text", nullable: true),
                    canopy = table.Column<string>(type: "text", nullable: true),
                    rock_outcrops = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    boulders = table.Column<string>(type: "text", nullable: true),
                    substrate = table.Column<string>(type: "text", nullable: true),
                    talus_scree = table.Column<bool>(type: "boolean", nullable: false),
                    lava_cap = table.Column<bool>(type: "boolean", nullable: false),
                    spring_seep = table.Column<bool>(type: "boolean", nullable: false),
                    pond = table.Column<bool>(type: "boolean", nullable: false),
                    other_wetlands = table.Column<string>(type: "text", nullable: true),
                    understory_vegetation = table.Column<string>(type: "text", nullable: true),
                    surveys = table.Column<int>(type: "integer", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<MultiPolygon>(type: "geometry(MultiPolygon,26710)", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_survey_areas", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_botanical_scopings_botanical_scoping~",
                        column: x => x.botanical_scoping_id,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_thp_areas_thp_area_id",
                        column: x => x.thp_area_id,
                        principalTable: "thp_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_survey_areas_hex160s",
                schema: "public",
                columns: table => new
                {
                    botanical_survey_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_survey_areas_hex160s", x => new { x.botanical_survey_area_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_hex160s_botanical_survey_areas_botan~",
                        column: x => x.botanical_survey_area_id,
                        principalTable: "botanical_survey_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_survey_areas_quad75s",
                schema: "public",
                columns: table => new
                {
                    botanical_survey_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_survey_areas_quad75s", x => new { x.botanical_survey_area_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_quad75s_botanical_survey_areas_botan~",
                        column: x => x.botanical_survey_area_id,
                        principalTable: "botanical_survey_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_survey_areas_watersheds",
                schema: "public",
                columns: table => new
                {
                    botanical_survey_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_survey_areas_watersheds", x => new { x.botanical_survey_area_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_watersheds_botanical_survey_areas_bo~",
                        column: x => x.botanical_survey_area_id,
                        principalTable: "botanical_survey_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_survey_areas_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_surveys",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_survey_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    survey_name = table.Column<string>(type: "text", nullable: true),
                    other_surveyors = table.Column<string>(type: "text", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    time_spent = table.Column<TimeSpan>(type: "interval", nullable: false),
                    manual_track = table.Column<bool>(type: "boolean", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    BotanicalScopingGuid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_surveys", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_botanical_scopings_BotanicalScopingGuid",
                        column: x => x.BotanicalScopingGuid,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_botanical_surveys_botanical_survey_areas_botanical_survey_a~",
                        column: x => x.botanical_survey_area_id,
                        principalTable: "botanical_survey_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_device_infos_device_info_id",
                        column: x => x.device_info_id,
                        principalTable: "device_infos",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_elements",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    record_type = table.Column<string>(type: "text", nullable: true),
                    botanical_scoping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_survey_area_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_point_of_interest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_plant_of_interest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_plant_list_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_elements", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_elements_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_botanical_points_of_interest_botanical_p~",
                        column: x => x.botanical_point_of_interest_id,
                        principalTable: "botanical_points_of_interest",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                        column: x => x.botanical_scoping_id,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_botanical_survey_areas_botanical_survey_~",
                        column: x => x.botanical_survey_area_id,
                        principalTable: "botanical_survey_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_botanical_surveys_botanical_survey_id",
                        column: x => x.botanical_survey_id,
                        principalTable: "botanical_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_elements_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_surveys_hex160s",
                schema: "public",
                columns: table => new
                {
                    botanical_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_surveys_hex160s", x => new { x.botanical_survey_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_botanical_surveys_hex160s_botanical_surveys_botanical_surve~",
                        column: x => x.botanical_survey_id,
                        principalTable: "botanical_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_surveys_quad75s",
                schema: "public",
                columns: table => new
                {
                    botanical_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_surveys_quad75s", x => new { x.botanical_survey_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_botanical_surveys_quad75s_botanical_surveys_botanical_surve~",
                        column: x => x.botanical_survey_id,
                        principalTable: "botanical_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_surveys_watersheds",
                schema: "public",
                columns: table => new
                {
                    botanical_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_surveys_watersheds", x => new { x.botanical_survey_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_botanical_surveys_watersheds_botanical_surveys_botanical_su~",
                        column: x => x.botanical_survey_id,
                        principalTable: "botanical_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_surveys_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_plants_list",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_plants_list", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_plants_list_botanical_elements_botanical_element_~",
                        column: x => x.botanical_element_id,
                        principalTable: "botanical_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_plants_list_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "botanical_plants_of_interest",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_found = table.Column<bool>(type: "boolean", nullable: false),
                    species_found_text = table.Column<string>(type: "text", nullable: true),
                    num_ind = table.Column<int>(type: "integer", nullable: false),
                    num_ind_max = table.Column<int>(type: "integer", nullable: false),
                    subsequent_visit = table.Column<bool>(type: "boolean", nullable: false),
                    existing_cnddb = table.Column<bool>(type: "boolean", nullable: false),
                    occ_num = table.Column<int>(type: "integer", nullable: false),
                    vegetative = table.Column<int>(type: "integer", nullable: false),
                    flowering = table.Column<int>(type: "integer", nullable: false),
                    fruiting = table.Column<int>(type: "integer", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    radius = table.Column<double>(type: "double precision", nullable: false),
                    site_quality = table.Column<string>(type: "text", nullable: true),
                    habitat = table.Column<string>(type: "text", nullable: true),
                    land_use = table.Column<string>(type: "text", nullable: true),
                    disturbances = table.Column<string>(type: "text", nullable: true),
                    threats = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_plants_of_interest", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_plants_of_interest_botanical_elements_botanical_e~",
                        column: x => x.botanical_element_id,
                        principalTable: "botanical_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_plants_of_interest_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_amphibian_element_id",
                table: "device_infos",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_amphibian_survey_id",
                table: "device_infos",
                column: "amphibian_survey_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_botanical_element_id",
                table: "device_infos",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_botanical_point_of_interest_id",
                table: "botanical_elements",
                column: "botanical_point_of_interest_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_botanical_scoping_id",
                table: "botanical_elements",
                column: "botanical_scoping_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_botanical_survey_area_id",
                table: "botanical_elements",
                column: "botanical_survey_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_botanical_survey_id",
                table: "botanical_elements",
                column: "botanical_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_district_id",
                table: "botanical_elements",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_hex160_id",
                table: "botanical_elements",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_quad75_id",
                table: "botanical_elements",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_user_id",
                table: "botanical_elements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_watershed_id",
                table: "botanical_elements",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_list_botanical_element_id",
                table: "botanical_plants_list",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_list_plant_species_id",
                table: "botanical_plants_list",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_of_interest_botanical_element_id",
                table: "botanical_plants_of_interest",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_of_interest_plant_species_id",
                table: "botanical_plants_of_interest",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_points_of_interest_amphibian_element_id",
                table: "botanical_points_of_interest",
                column: "amphibian_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_region_id",
                table: "botanical_scopings",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_thp_area_id",
                table: "botanical_scopings",
                column: "thp_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_user_id",
                table: "botanical_scopings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_districts_district_id",
                schema: "public",
                table: "botanical_scopings_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_quad75s_quad75_id",
                schema: "public",
                table: "botanical_scopings_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scopings_watersheds_watershed_id",
                schema: "public",
                table: "botanical_scopings_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_botanical_scoping_id",
                table: "botanical_survey_areas",
                column: "botanical_scoping_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_district_id",
                table: "botanical_survey_areas",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_thp_area_id",
                table: "botanical_survey_areas",
                column: "thp_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_user_id",
                table: "botanical_survey_areas",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_hex160s_hex160_id",
                schema: "public",
                table: "botanical_survey_areas_hex160s",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_quad75s_quad75_id",
                schema: "public",
                table: "botanical_survey_areas_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_survey_areas_watersheds_watershed_id",
                schema: "public",
                table: "botanical_survey_areas_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_botanical_survey_area_id",
                table: "botanical_surveys",
                column: "botanical_survey_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_BotanicalScopingGuid",
                table: "botanical_surveys",
                column: "BotanicalScopingGuid");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_device_info_id",
                table: "botanical_surveys",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_district_id",
                table: "botanical_surveys",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_user_id",
                table: "botanical_surveys",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_hex160s_hex160_id",
                schema: "public",
                table: "botanical_surveys_hex160s",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_quad75s_quad75_id",
                schema: "public",
                table: "botanical_surveys_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_surveys_watersheds_watershed_id",
                schema: "public",
                table: "botanical_surveys_watersheds",
                column: "watershed_id");

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

            migrationBuilder.DropTable(
                name: "botanical_plants_list");

            migrationBuilder.DropTable(
                name: "botanical_plants_of_interest");

            migrationBuilder.DropTable(
                name: "botanical_scopings_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_scopings_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_scopings_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_survey_areas_hex160s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_survey_areas_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_survey_areas_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_surveys_hex160s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_surveys_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_surveys_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "botanical_elements");

            migrationBuilder.DropTable(
                name: "botanical_points_of_interest");

            migrationBuilder.DropTable(
                name: "botanical_surveys");

            migrationBuilder.DropTable(
                name: "botanical_survey_areas");

            migrationBuilder.DropTable(
                name: "botanical_scopings");

            migrationBuilder.DropTable(
                name: "thp_areas");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_amphibian_element_id",
                table: "device_infos");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_amphibian_survey_id",
                table: "device_infos");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_botanical_element_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "species_present",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "species_present",
                table: "site_calling_repositories");

            migrationBuilder.DropColumn(
                name: "amphibian_element_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "amphibian_survey_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "botanical_element_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "horizontal_accuracy",
                table: "device_infos");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_device_info_id",
                table: "amphibian_surveys",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_device_info_id",
                table: "amphibian_elements",
                column: "device_info_id");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_device_infos_device_info_id",
                table: "amphibian_elements",
                column: "device_info_id",
                principalTable: "device_infos",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_device_infos_device_info_id",
                table: "amphibian_surveys",
                column: "device_info_id",
                principalTable: "device_infos",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
