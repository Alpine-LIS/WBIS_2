using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ForestCarnivoreCameraStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ForestCarnivoreCameraStationGuid",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "forest_carnivore_camera_station_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "forest_carnivore_camera_stations",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    station_id = table.Column<string>(type: "text", nullable: false),
                    station_id_year = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_modified_id = table.Column<Guid>(type: "uuid", nullable: true),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    repository = table.Column<bool>(type: "boolean", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: true)
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
                name: "carnivore_occurrences",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    wildlife_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    forest_carnivore_camera_station_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_pictures_ForestCarnivoreCameraStationGuid",
                table: "pictures",
                column: "ForestCarnivoreCameraStationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_forest_carnivore_camera_station_id",
                table: "device_infos",
                column: "forest_carnivore_camera_station_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_carnivore_occurrences_forest_carnivore_camera_station_id",
                table: "carnivore_occurrences",
                column: "forest_carnivore_camera_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_carnivore_occurrences_wildlife_species_id",
                table: "carnivore_occurrences",
                column: "wildlife_species_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_forest_carnivore_camera_stations_forest_carniv~",
                table: "device_infos",
                column: "forest_carnivore_camera_station_id",
                principalTable: "forest_carnivore_camera_stations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_ForestCarnivoreCa~",
                table: "pictures",
                column: "ForestCarnivoreCameraStationGuid",
                principalTable: "forest_carnivore_camera_stations",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_forest_carnivore_camera_stations_forest_carniv~",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_ForestCarnivoreCa~",
                table: "pictures");

            migrationBuilder.DropTable(
                name: "carnivore_occurrences");

            migrationBuilder.DropTable(
                name: "forest_carnivore_camera_stations");

            migrationBuilder.DropIndex(
                name: "IX_pictures_ForestCarnivoreCameraStationGuid",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_forest_carnivore_camera_station_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "ForestCarnivoreCameraStationGuid",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "forest_carnivore_camera_station_id",
                table: "device_infos");
        }
    }
}
