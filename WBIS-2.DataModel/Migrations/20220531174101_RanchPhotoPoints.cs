using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class RanchPhotoPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RanchPhotoPointGuid",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ranch_photo_point_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ranch_photo_points",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ranch = table.Column<string>(type: "text", nullable: false),
                    stream_name = table.Column<string>(type: "text", nullable: false),
                    site_type = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    photo_id = table.Column<double>(type: "double precision", nullable: false),
                    image_number = table.Column<string>(type: "text", nullable: false),
                    azimuth = table.Column<double>(type: "double precision", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_pictures_RanchPhotoPointGuid",
                table: "pictures",
                column: "RanchPhotoPointGuid");

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_ranch_photo_point_id",
                table: "device_infos",
                column: "ranch_photo_point_id",
                unique: true);

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
                name: "FK_device_infos_ranch_photo_points_ranch_photo_point_id",
                table: "device_infos",
                column: "ranch_photo_point_id",
                principalTable: "ranch_photo_points",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_ranch_photo_points_RanchPhotoPointGuid",
                table: "pictures",
                column: "RanchPhotoPointGuid",
                principalTable: "ranch_photo_points",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_ranch_photo_points_ranch_photo_point_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_ranch_photo_points_RanchPhotoPointGuid",
                table: "pictures");

            migrationBuilder.DropTable(
                name: "ranch_photo_points");

            migrationBuilder.DropIndex(
                name: "IX_pictures_RanchPhotoPointGuid",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_ranch_photo_point_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "RanchPhotoPointGuid",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "ranch_photo_point_id",
                table: "device_infos");
        }
    }
}
