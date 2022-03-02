using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AddOwlBanding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "owl_banding_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "owl_bandings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bands = table.Column<string>(type: "text", nullable: false),
                    record_type = table.Column<string>(type: "text", nullable: true),
                    bird_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    banding_leg = table.Column<string>(type: "text", nullable: true),
                    banding_pattern = table.Column<string>(type: "text", nullable: true),
                    usfws_band_num = table.Column<string>(type: "text", nullable: true),
                    usfws_band_color = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bander = table.Column<string>(type: "text", nullable: true),
                    capturer = table.Column<string>(type: "text", nullable: true),
                    trap_code = table.Column<string>(type: "text", nullable: true),
                    capture_method = table.Column<string>(type: "text", nullable: true),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    starting_lat = table.Column<double>(type: "double precision", nullable: false),
                    starting_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    temperature = table.Column<double>(type: "double precision", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gps_tag_id = table.Column<string>(type: "text", nullable: true),
                    sex = table.Column<string>(type: "text", nullable: true),
                    age_class = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    wing_chord = table.Column<double>(type: "double precision", nullable: false),
                    tail_length = table.Column<double>(type: "double precision", nullable: false),
                    footpad = table.Column<double>(type: "double precision", nullable: false),
                    blood = table.Column<bool>(type: "boolean", nullable: false),
                    oral_sample = table.Column<bool>(type: "boolean", nullable: false),
                    ectoparasites = table.Column<bool>(type: "boolean", nullable: false),
                    feathers = table.Column<bool>(type: "boolean", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owl_bandings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_owl_bandings_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_bird_species_bird_species_id",
                        column: x => x.bird_species_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_protection_zones_preotection_zone_id",
                        column: x => x.preotection_zone_id,
                        principalTable: "protection_zones",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_owl_banding_id",
                table: "device_infos",
                column: "owl_banding_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_bird_species_id",
                table: "owl_bandings",
                column: "bird_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_hex160_id",
                table: "owl_bandings",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_preotection_zone_id",
                table: "owl_bandings",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_user_id",
                table: "owl_bandings",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_owl_bandings_owl_banding_id",
                table: "device_infos");

            migrationBuilder.DropTable(
                name: "owl_bandings");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_owl_banding_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "owl_banding_id",
                table: "device_infos");
        }
    }
}
