using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AddedAmphibianSurveys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "starting_lon",
                table: "owl_bandings",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "starting_lat",
                table: "owl_bandings",
                newName: "lat");

            migrationBuilder.CreateTable(
                name: "amphibian_species",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    species_name = table.Column<string>(type: "text", nullable: true),
                    species_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_species", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_surveys",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<string>(type: "text", nullable: false),
                    surveyors = table.Column<string>(type: "text", nullable: false),
                    date_time = table.Column<string>(type: "text", nullable: true),
                    lake_stream_name = table.Column<string>(type: "text", nullable: true),
                    water_type = table.Column<string>(type: "text", nullable: true),
                    seasonality_if_flow = table.Column<string>(type: "text", nullable: true),
                    planning_watershed = table.Column<string>(type: "text", nullable: true),
                    county = table.Column<string>(type: "text", nullable: true),
                    elevation = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    weather = table.Column<string>(type: "text", nullable: true),
                    wind = table.Column<string>(type: "text", nullable: true),
                    location_comments = table.Column<string>(type: "text", nullable: true),
                    canopy_closure = table.Column<string>(type: "text", nullable: true),
                    stream_gradient = table.Column<string>(type: "text", nullable: true),
                    silt = table.Column<string>(type: "text", nullable: true),
                    sand = table.Column<string>(type: "text", nullable: true),
                    gravel = table.Column<string>(type: "text", nullable: true),
                    cobble = table.Column<string>(type: "text", nullable: true),
                    boulders = table.Column<string>(type: "text", nullable: true),
                    bedrock = table.Column<string>(type: "text", nullable: true),
                    pool = table.Column<string>(type: "text", nullable: true),
                    riffle = table.Column<string>(type: "text", nullable: true),
                    run = table.Column<string>(type: "text", nullable: true),
                    est_avg_stream_width = table.Column<string>(type: "text", nullable: true),
                    water_temp = table.Column<string>(type: "text", nullable: true),
                    air_temp = table.Column<string>(type: "text", nullable: true),
                    flow = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_surveys", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_device_infos_device_info_id",
                        column: x => x.device_info_id,
                        principalTable: "device_infos",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_elements",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    record_type = table.Column<string>(type: "text", nullable: true),
                    amphibian_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_location_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_point_of_interest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_elements", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                        column: x => x.amphibian_survey_id,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_device_infos_device_info_id",
                        column: x => x.device_info_id,
                        principalTable: "device_infos",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_locations_found",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    point_of_interest = table.Column<string>(type: "text", nullable: false),
                    amphibian_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number_of_adults = table.Column<double>(type: "double precision", nullable: false),
                    number_of_subadults = table.Column<double>(type: "double precision", nullable: false),
                    number_of_larve = table.Column<double>(type: "double precision", nullable: false),
                    number_of_egg_masses = table.Column<double>(type: "double precision", nullable: false),
                    visual = table.Column<bool>(type: "boolean", nullable: false),
                    aural = table.Column<bool>(type: "boolean", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    AmphibianSurveyGuid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_locations_found", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_locations_found_amphibian_elements_amphibian_elem~",
                        column: x => x.amphibian_element_id,
                        principalTable: "amphibian_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                        column: x => x.amphibian_species_id,
                        principalTable: "amphibian_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_locations_found_amphibian_surveys_AmphibianSurvey~",
                        column: x => x.AmphibianSurveyGuid,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "amphibian_points_of_interest",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    point_of_interest = table.Column<string>(type: "text", nullable: false),
                    other_wildlife_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    AmphibianSurveyGuid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_points_of_interest", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_points_of_interest_amphibian_elements_amphibian_e~",
                        column: x => x.amphibian_element_id,
                        principalTable: "amphibian_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                        column: x => x.other_wildlife_id,
                        principalTable: "amphibian_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_points_of_interest_amphibian_surveys_AmphibianSur~",
                        column: x => x.AmphibianSurveyGuid,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_amphibian_survey_id",
                table: "amphibian_elements",
                column: "amphibian_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_device_info_id",
                table: "amphibian_elements",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_hex160_id",
                table: "amphibian_elements",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_user_id",
                table: "amphibian_elements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_amphibian_element_id",
                table: "amphibian_locations_found",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_amphibian_species_id",
                table: "amphibian_locations_found",
                column: "amphibian_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_AmphibianSurveyGuid",
                table: "amphibian_locations_found",
                column: "AmphibianSurveyGuid");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_amphibian_element_id",
                table: "amphibian_points_of_interest",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_AmphibianSurveyGuid",
                table: "amphibian_points_of_interest",
                column: "AmphibianSurveyGuid");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_other_wildlife_id",
                table: "amphibian_points_of_interest",
                column: "other_wildlife_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_device_info_id",
                table: "amphibian_surveys",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_hex160_id",
                table: "amphibian_surveys",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_user_id",
                table: "amphibian_surveys",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amphibian_locations_found");

            migrationBuilder.DropTable(
                name: "amphibian_points_of_interest");

            migrationBuilder.DropTable(
                name: "amphibian_elements");

            migrationBuilder.DropTable(
                name: "amphibian_species");

            migrationBuilder.DropTable(
                name: "amphibian_surveys");

            migrationBuilder.RenameColumn(
                name: "lon",
                table: "owl_bandings",
                newName: "starting_lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "owl_bandings",
                newName: "starting_lat");
        }
    }
}
