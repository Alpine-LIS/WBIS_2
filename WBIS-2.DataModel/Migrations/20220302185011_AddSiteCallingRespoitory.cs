using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AddSiteCallingRespoitory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_site_callings_guid",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_user_locations_site_calling_detections_guid",
                table: "user_locations");

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
                name: "site_calling_repository_id",
                table: "device_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "site_calling_repositories",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry", nullable: false),
                    starting_lat = table.Column<double>(type: "double precision", nullable: false),
                    starting_lon = table.Column<double>(type: "double precision", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sunset_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    survey_type1 = table.Column<string>(type: "text", nullable: false),
                    survey_type2 = table.Column<string>(type: "text", nullable: false),
                    bird_species_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<string>(type: "text", nullable: true),
                    pass_number = table.Column<int>(type: "integer", nullable: false),
                    pz_pass_number = table.Column<int>(type: "integer", nullable: false),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    yearly_activity_center = table.Column<bool>(type: "boolean", nullable: false),
                    wind = table.Column<string>(type: "text", nullable: true),
                    precipitation = table.Column<string>(type: "text", nullable: true),
                    target_species_present = table.Column<bool>(type: "boolean", nullable: false),
                    site_calling_repository_detection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    occupancy_status = table.Column<string>(type: "text", nullable: false),
                    nesting_status = table.Column<string>(type: "text", nullable: true),
                    reproductive_status = table.Column<string>(type: "text", nullable: true),
                    nest_tree = table.Column<bool>(type: "boolean", nullable: false),
                    nest_type = table.Column<string>(type: "text", nullable: true),
                    tree_species = table.Column<string>(type: "text", nullable: true),
                    dbh = table.Column<double>(type: "double precision", nullable: false),
                    nest_height = table.Column<double>(type: "double precision", nullable: false),
                    tree_tagged = table.Column<bool>(type: "boolean", nullable: false),
                    moused = table.Column<bool>(type: "boolean", nullable: false),
                    area_description = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    site_calling_repository_track_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repository_detections",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bird_species_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_method = table.Column<string>(type: "text", nullable: false),
                    geometry = table.Column<Point>(type: "geometry", nullable: false),
                    detection_lat = table.Column<double>(type: "double precision", nullable: false),
                    detection_lon = table.Column<double>(type: "double precision", nullable: false),
                    user_location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    bearing = table.Column<double>(type: "double precision", nullable: false),
                    estimated_location = table.Column<bool>(type: "boolean", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<string>(type: "text", nullable: false),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    species_site = table.Column<string>(type: "text", nullable: true),
                    male_banding_leg = table.Column<string>(type: "text", nullable: true),
                    male_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    female_banding_leg = table.Column<string>(type: "text", nullable: true),
                    female_banding_pattern = table.Column<string>(type: "text", nullable: true)
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
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry", nullable: true)
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
                name: "IX_user_locations_site_calling_detection_id",
                table: "user_locations",
                column: "site_calling_detection_id",
                unique: true);

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
                name: "IX_device_infos_site_calling_id",
                table: "device_infos",
                column: "site_calling_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_site_calling_repository_id",
                table: "device_infos",
                column: "site_calling_repository_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_bird_species_survey_id",
                table: "site_calling_repositories",
                column: "bird_species_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_hex160_id",
                table: "site_calling_repositories",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_preotection_zone_id",
                table: "site_calling_repositories",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_user_id",
                table: "site_calling_repositories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repository_detections_bird_species_found_id",
                table: "site_calling_repository_detections",
                column: "bird_species_found_id");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_site_calling_repositories_site_calling_reposit~",
                table: "device_infos",
                column: "site_calling_repository_id",
                principalTable: "site_calling_repositories",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_site_callings_site_calling_id",
                table: "device_infos",
                column: "site_calling_id",
                principalTable: "site_callings",
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
                name: "FK_user_locations_site_calling_detections_site_calling_detecti~",
                table: "user_locations",
                column: "site_calling_detection_id",
                principalTable: "site_calling_detections",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_site_calling_repositories_site_calling_reposit~",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_site_callings_site_calling_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_other_wildlife_records_site_calling_repositories_site_calli~",
                table: "other_wildlife_records");

            migrationBuilder.DropForeignKey(
                name: "FK_user_locations_site_calling_detections_site_calling_detecti~",
                table: "user_locations");

            migrationBuilder.DropForeignKey(
                name: "FK_user_locations_site_calling_repository_detections_site_call~",
                table: "user_locations");

            migrationBuilder.DropTable(
                name: "site_calling_repository_detections");

            migrationBuilder.DropTable(
                name: "site_calling_repository_tracks");

            migrationBuilder.DropTable(
                name: "site_calling_repositories");

            migrationBuilder.DropIndex(
                name: "IX_user_locations_site_calling_detection_id",
                table: "user_locations");

            migrationBuilder.DropIndex(
                name: "IX_user_locations_site_calling_repository_detection_id",
                table: "user_locations");

            migrationBuilder.DropIndex(
                name: "IX_other_wildlife_records_site_calling_repository_id",
                table: "other_wildlife_records");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_site_calling_id",
                table: "device_infos");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_site_calling_repository_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "site_calling_repository_detection_id",
                table: "user_locations");

            migrationBuilder.DropColumn(
                name: "site_calling_repository_id",
                table: "other_wildlife_records");

            migrationBuilder.DropColumn(
                name: "site_calling_repository_id",
                table: "device_infos");

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_site_callings_guid",
                table: "device_infos",
                column: "guid",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_locations_site_calling_detections_guid",
                table: "user_locations",
                column: "guid",
                principalTable: "site_calling_detections",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
