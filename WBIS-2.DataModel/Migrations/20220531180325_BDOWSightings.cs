using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class BDOWSightings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "bdow_sighting_id",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "bdow_sighting_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bdow_sightings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    moon_phase = table.Column<string>(type: "text", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    barred_owl_territory_id = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: true),
                    wing_chord = table.Column<double>(type: "double precision", nullable: false),
                    tail_length = table.Column<double>(type: "double precision", nullable: false),
                    foot_pad = table.Column<double>(type: "double precision", nullable: false),
                    sge = table.Column<string>(type: "text", nullable: true),
                    shotgun = table.Column<bool>(type: "boolean", nullable: false),
                    shotgun_text = table.Column<string>(type: "text", nullable: true),
                    ectoparasites_noticed = table.Column<bool>(type: "boolean", nullable: false),
                    shots_taken = table.Column<int>(type: "integer", nullable: false),
                    feathers_collected = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_pictures_bdow_sighting_id",
                table: "pictures",
                column: "bdow_sighting_id");

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_bdow_sighting_id",
                table: "device_infos",
                column: "bdow_sighting_id",
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

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_bdow_sightings_bdow_sighting_id",
                table: "device_infos",
                column: "bdow_sighting_id",
                principalTable: "bdow_sightings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_bdow_sightings_bdow_sighting_id",
                table: "pictures",
                column: "bdow_sighting_id",
                principalTable: "bdow_sightings",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_bdow_sightings_bdow_sighting_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_bdow_sightings_bdow_sighting_id",
                table: "pictures");

            migrationBuilder.DropTable(
                name: "bdow_sightings");

            migrationBuilder.DropIndex(
                name: "IX_pictures_bdow_sighting_id",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_bdow_sighting_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "bdow_sighting_id",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "bdow_sighting_id",
                table: "device_infos");
        }
    }
}
