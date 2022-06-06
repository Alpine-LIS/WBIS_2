using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class OtherWildlifeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spi_ggows",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    spi_id = table.Column<string>(type: "text", nullable: true),
                    nest_name = table.Column<string>(type: "text", nullable: true),
                    territory = table.Column<string>(type: "text", nullable: true),
                    monitoring_group = table.Column<string>(type: "text", nullable: true),
                    habitat_area = table.Column<string>(type: "text", nullable: true),
                    unique_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    twn = table.Column<string>(type: "text", nullable: true),
                    rge = table.Column<string>(type: "text", nullable: true),
                    sec = table.Column<string>(type: "text", nullable: true),
                    quarter = table.Column<string>(type: "text", nullable: true),
                    sixteenth = table.Column<string>(type: "text", nullable: true),
                    utm_zone = table.Column<string>(type: "text", nullable: true),
                    utm_e = table.Column<string>(type: "text", nullable: true),
                    utm_n = table.Column<string>(type: "text", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    results = table.Column<string>(type: "text", nullable: true),
                    pair = table.Column<string>(type: "text", nullable: true),
                    num_nestlings = table.Column<int>(type: "integer", nullable: false),
                    num_fledglings = table.Column<int>(type: "integer", nullable: false),
                    nest = table.Column<string>(type: "text", nullable: true),
                    surveyors = table.Column<string>(type: "text", nullable: true),
                    year_measured = table.Column<int>(type: "integer", nullable: false),
                    year_used = table.Column<int>(type: "integer", nullable: false),
                    gid_mapped = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_ggows", x => x.guid);
                    table.ForeignKey(
                        name: "FK_spi_ggows_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_spi_ggows_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "spi_nogos",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    dist_id = table.Column<string>(type: "text", nullable: true),
                    nest_id = table.Column<int>(type: "integer", nullable: false),
                    territory = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: false),
                    nest_name = table.Column<string>(type: "text", nullable: true),
                    territory_status = table.Column<string>(type: "text", nullable: true),
                    pair = table.Column<string>(type: "text", nullable: true),
                    nest = table.Column<string>(type: "text", nullable: true),
                    young = table.Column<string>(type: "text", nullable: true),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    township = table.Column<string>(type: "text", nullable: true),
                    range = table.Column<string>(type: "text", nullable: true),
                    section = table.Column<string>(type: "text", nullable: true),
                    quarter = table.Column<string>(type: "text", nullable: true),
                    sixteenth = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    surveyor = table.Column<string>(type: "text", nullable: true),
                    owner = table.Column<string>(type: "text", nullable: true),
                    unique_id = table.Column<int>(type: "integer", nullable: false),
                    _300m_search = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    transmitter = table.Column<string>(type: "text", nullable: true),
                    ref_loc = table.Column<string>(type: "text", nullable: true),
                    usfs_exchange = table.Column<string>(type: "text", nullable: true),
                    wsid = table.Column<int>(type: "integer", nullable: false),
                    utm_northing_coordinate = table.Column<int>(type: "integer", nullable: false),
                    utm_easting_coordinate = table.Column<int>(type: "integer", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_nogos", x => x.guid);
                    table.ForeignKey(
                        name: "FK_spi_nogos_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_spi_nogos_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "spi_spows",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false),
                    dist_id = table.Column<string>(type: "text", nullable: true),
                    cdfw_id = table.Column<string>(type: "text", nullable: true),
                    territory = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: false),
                    bird_status = table.Column<string>(type: "text", nullable: true),
                    hcp_status = table.Column<string>(type: "text", nullable: true),
                    hcp_status_2 = table.Column<string>(type: "text", nullable: true),
                    occupied = table.Column<string>(type: "text", nullable: true),
                    num_seen = table.Column<int>(type: "integer", nullable: false),
                    adult_subadult_count = table.Column<int>(type: "integer", nullable: false),
                    pair = table.Column<string>(type: "text", nullable: true),
                    nest = table.Column<string>(type: "text", nullable: true),
                    young = table.Column<string>(type: "text", nullable: true),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    yac = table.Column<string>(type: "text", nullable: true),
                    response = table.Column<string>(type: "text", nullable: true),
                    male_response = table.Column<string>(type: "text", nullable: true),
                    female_response = table.Column<string>(type: "text", nullable: true),
                    unknown_sex_response = table.Column<string>(type: "text", nullable: true),
                    barred_owl_response = table.Column<string>(type: "text", nullable: true),
                    habitat_cross_plot = table.Column<string>(type: "text", nullable: true),
                    study_area = table.Column<string>(type: "text", nullable: true),
                    unique_id = table.Column<int>(type: "integer", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    township = table.Column<string>(type: "text", nullable: true),
                    range = table.Column<string>(type: "text", nullable: true),
                    section = table.Column<string>(type: "text", nullable: true),
                    quarter = table.Column<string>(type: "text", nullable: true),
                    sixteenth = table.Column<string>(type: "text", nullable: true),
                    source = table.Column<string>(type: "text", nullable: true),
                    sub_species1 = table.Column<string>(type: "text", nullable: true),
                    age_sex = table.Column<string>(type: "text", nullable: true),
                    visit_type = table.Column<string>(type: "text", nullable: true),
                    detection_type = table.Column<string>(type: "text", nullable: true),
                    observer = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    ws_id = table.Column<int>(type: "integer", nullable: false),
                    utm_northing_coordinate = table.Column<int>(type: "integer", nullable: false),
                    utm_easting_coordinate = table.Column<int>(type: "integer", nullable: false),
                    wbis_mapped_location = table.Column<string>(type: "text", nullable: true),
                    hex500_id = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_spows", x => x.guid);
                    table.ForeignKey(
                        name: "FK_spi_spows_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_spi_spows_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "spi_wildlife_sightings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    wildlife_species = table.Column<string>(type: "text", nullable: true),
                    genus = table.Column<string>(type: "text", nullable: true),
                    species = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: false),
                    num_observed = table.Column<int>(type: "integer", nullable: false),
                    activity_observed = table.Column<string>(type: "text", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    iucn_rating = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_wildlife_sightings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_spi_wildlife_sightings_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_spi_wildlife_sightings_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_spi_ggows_district_id",
                table: "spi_ggows",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_ggows_watershed_id",
                table: "spi_ggows",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_nogos_district_id",
                table: "spi_nogos",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_nogos_watershed_id",
                table: "spi_nogos",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_spows_district_id",
                table: "spi_spows",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_spows_watershed_id",
                table: "spi_spows",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_wildlife_sightings_district_id",
                table: "spi_wildlife_sightings",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_wildlife_sightings_watershed_id",
                table: "spi_wildlife_sightings",
                column: "watershed_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spi_ggows");

            migrationBuilder.DropTable(
                name: "spi_nogos");

            migrationBuilder.DropTable(
                name: "spi_spows");

            migrationBuilder.DropTable(
                name: "spi_wildlife_sightings");
        }
    }
}
