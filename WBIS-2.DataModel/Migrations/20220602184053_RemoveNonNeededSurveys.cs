using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class RemoveNonNeededSurveys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_bdow_sightings_bdow_sighting_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_do_monitorings_do_monitoring_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_forest_carnivore_camera_stations_forest_carniv~",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_ranch_photo_points_ranch_photo_point_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_bdow_sightings_bdow_sighting_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_do_monitorings_do_monitoring_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_forest_carnivore_~",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_ranch_photo_points_ranch_photo_point_id",
                table: "pictures");

            migrationBuilder.DropTable(
                name: "bdow_sightings");

            migrationBuilder.DropTable(
                name: "carnivore_occurrences");

            migrationBuilder.DropTable(
                name: "do_monitorings");

            migrationBuilder.DropTable(
                name: "ranch_photo_points");

            migrationBuilder.DropTable(
                name: "forest_carnivore_camera_stations");

            migrationBuilder.DropIndex(
                name: "IX_pictures_bdow_sighting_id",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_pictures_do_monitoring_id",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_pictures_forest_carnivore_camera_station_id",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_pictures_ranch_photo_point_id",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_bdow_sighting_id",
                table: "device_infos");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_do_monitoring_id",
                table: "device_infos");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_forest_carnivore_camera_station_id",
                table: "device_infos");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_ranch_photo_point_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "bdow_sighting_id",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "do_monitoring_id",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "forest_carnivore_camera_station_id",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "ranch_photo_point_id",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "bdow_sighting_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "do_monitoring_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "forest_carnivore_camera_station_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "ranch_photo_point_id",
                table: "device_infos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "bdow_sighting_id",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "do_monitoring_id",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "forest_carnivore_camera_station_id",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ranch_photo_point_id",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "bdow_sighting_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "do_monitoring_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "forest_carnivore_camera_station_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ranch_photo_point_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bdow_sightings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: true),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_modified_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true),
                    age = table.Column<string>(type: "text", nullable: true),
                    barred_owl_territory_id = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    ectoparasites_noticed = table.Column<bool>(type: "boolean", nullable: false),
                    feathers_collected = table.Column<int>(type: "integer", nullable: false),
                    foot_pad = table.Column<double>(type: "double precision", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    moon_phase = table.Column<string>(type: "text", nullable: false),
                    repository = table.Column<bool>(type: "boolean", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: true),
                    shotgun = table.Column<bool>(type: "boolean", nullable: false),
                    shotgun_text = table.Column<string>(type: "text", nullable: true),
                    shots_taken = table.Column<int>(type: "integer", nullable: false),
                    tail_length = table.Column<double>(type: "double precision", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    wing_chord = table.Column<double>(type: "double precision", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bdow_sightings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_bdow_sightings_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_bdow_sightings_application_users_user_modified_id",
                        column: x => x.user_modified_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_bdow_sightings_bird_species_species_id",
                        column: x => x.species_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bdow_sightings_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_bdow_sightings_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_bdow_sightings_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_bdow_sightings_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "do_monitorings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_modified_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true),
                    air_temperature = table.Column<double>(type: "double precision", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    reading_mgl = table.Column<double>(type: "double precision", nullable: false),
                    reading_ppm = table.Column<double>(type: "double precision", nullable: false),
                    reading_pcnt = table.Column<double>(type: "double precision", nullable: false),
                    repository = table.Column<bool>(type: "boolean", nullable: false),
                    temperature = table.Column<double>(type: "double precision", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    ph = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_do_monitorings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_do_monitorings_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_do_monitorings_application_users_user_modified_id",
                        column: x => x.user_modified_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_do_monitorings_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_do_monitorings_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_do_monitorings_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_do_monitorings_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "forest_carnivore_camera_stations",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_modified_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    repository = table.Column<bool>(type: "boolean", nullable: false),
                    station_id = table.Column<string>(type: "text", nullable: false),
                    station_id_year = table.Column<string>(type: "text", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forest_carnivore_camera_stations", x => x.guid);
                    table.ForeignKey(
                        name: "FK_forest_carnivore_camera_stations_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_forest_carnivore_camera_stations_application_users_user_mod~",
                        column: x => x.user_modified_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_forest_carnivore_camera_stations_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_forest_carnivore_camera_stations_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_forest_carnivore_camera_stations_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_forest_carnivore_camera_stations_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "ranch_photo_points",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_modified_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true),
                    azimuth = table.Column<double>(type: "double precision", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    image_number = table.Column<string>(type: "text", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    photo_id = table.Column<double>(type: "double precision", nullable: false),
                    ranch = table.Column<string>(type: "text", nullable: false),
                    repository = table.Column<bool>(type: "boolean", nullable: false),
                    site_type = table.Column<string>(type: "text", nullable: false),
                    stream_name = table.Column<string>(type: "text", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranch_photo_points", x => x.guid);
                    table.ForeignKey(
                        name: "FK_ranch_photo_points_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_ranch_photo_points_application_users_user_modified_id",
                        column: x => x.user_modified_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_ranch_photo_points_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_ranch_photo_points_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_ranch_photo_points_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_ranch_photo_points_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "carnivore_occurrences",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    forest_carnivore_camera_station_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wildlife_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carnivore_occurrences", x => x.guid);
                    table.ForeignKey(
                        name: "FK_carnivore_occurrences_forest_carnivore_camera_stations_fore~",
                        column: x => x.forest_carnivore_camera_station_id,
                        principalTable: "forest_carnivore_camera_stations",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carnivore_occurrences_wildlife_species_wildlife_species_id",
                        column: x => x.wildlife_species_id,
                        principalTable: "wildlife_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pictures_bdow_sighting_id",
                table: "pictures",
                column: "bdow_sighting_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_do_monitoring_id",
                table: "pictures",
                column: "do_monitoring_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_forest_carnivore_camera_station_id",
                table: "pictures",
                column: "forest_carnivore_camera_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_ranch_photo_point_id",
                table: "pictures",
                column: "ranch_photo_point_id");

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_bdow_sighting_id",
                table: "device_infos",
                column: "bdow_sighting_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_do_monitoring_id",
                table: "device_infos",
                column: "do_monitoring_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_forest_carnivore_camera_station_id",
                table: "device_infos",
                column: "forest_carnivore_camera_station_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_ranch_photo_point_id",
                table: "device_infos",
                column: "ranch_photo_point_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_district_id",
                table: "bdow_sightings",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_hex160_id",
                table: "bdow_sightings",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_quad75_id",
                table: "bdow_sightings",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_species_id",
                table: "bdow_sightings",
                column: "species_id");

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_user_id",
                table: "bdow_sightings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_user_modified_id",
                table: "bdow_sightings",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_bdow_sightings_watershed_id",
                table: "bdow_sightings",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_carnivore_occurrences_forest_carnivore_camera_station_id",
                table: "carnivore_occurrences",
                column: "forest_carnivore_camera_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_carnivore_occurrences_wildlife_species_id",
                table: "carnivore_occurrences",
                column: "wildlife_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_monitorings_district_id",
                table: "do_monitorings",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_monitorings_hex160_id",
                table: "do_monitorings",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_monitorings_quad75_id",
                table: "do_monitorings",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_monitorings_user_id",
                table: "do_monitorings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_monitorings_user_modified_id",
                table: "do_monitorings",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_monitorings_watershed_id",
                table: "do_monitorings",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_forest_carnivore_camera_stations_district_id",
                table: "forest_carnivore_camera_stations",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_forest_carnivore_camera_stations_hex160_id",
                table: "forest_carnivore_camera_stations",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_forest_carnivore_camera_stations_quad75_id",
                table: "forest_carnivore_camera_stations",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_forest_carnivore_camera_stations_user_id",
                table: "forest_carnivore_camera_stations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_forest_carnivore_camera_stations_user_modified_id",
                table: "forest_carnivore_camera_stations",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_forest_carnivore_camera_stations_watershed_id",
                table: "forest_carnivore_camera_stations",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_ranch_photo_points_district_id",
                table: "ranch_photo_points",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_ranch_photo_points_hex160_id",
                table: "ranch_photo_points",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_ranch_photo_points_quad75_id",
                table: "ranch_photo_points",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_ranch_photo_points_user_id",
                table: "ranch_photo_points",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ranch_photo_points_user_modified_id",
                table: "ranch_photo_points",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_ranch_photo_points_watershed_id",
                table: "ranch_photo_points",
                column: "watershed_id");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_bdow_sightings_bdow_sighting_id",
                table: "device_infos",
                column: "bdow_sighting_id",
                principalTable: "bdow_sightings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_do_monitorings_do_monitoring_id",
                table: "device_infos",
                column: "do_monitoring_id",
                principalTable: "do_monitorings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_forest_carnivore_camera_stations_forest_carniv~",
                table: "device_infos",
                column: "forest_carnivore_camera_station_id",
                principalTable: "forest_carnivore_camera_stations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_ranch_photo_points_ranch_photo_point_id",
                table: "device_infos",
                column: "ranch_photo_point_id",
                principalTable: "ranch_photo_points",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_bdow_sightings_bdow_sighting_id",
                table: "pictures",
                column: "bdow_sighting_id",
                principalTable: "bdow_sightings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_do_monitorings_do_monitoring_id",
                table: "pictures",
                column: "do_monitoring_id",
                principalTable: "do_monitorings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_forest_carnivore_~",
                table: "pictures",
                column: "forest_carnivore_camera_station_id",
                principalTable: "forest_carnivore_camera_stations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_ranch_photo_points_ranch_photo_point_id",
                table: "pictures",
                column: "ranch_photo_point_id",
                principalTable: "ranch_photo_points",
                principalColumn: "guid");
        }
    }
}
