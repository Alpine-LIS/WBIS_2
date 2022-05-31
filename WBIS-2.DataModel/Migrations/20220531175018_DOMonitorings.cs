using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class DOMonitorings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DOMonitoringGuid",
                table: "pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "do_monitoring_id",
                table: "device_infos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "do_monitorings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    reading_pcnt = table.Column<double>(type: "double precision", nullable: false),
                    reading_ppm = table.Column<double>(type: "double precision", nullable: false),
                    reading_mgl = table.Column<double>(type: "double precision", nullable: false),
                    temperature = table.Column<double>(type: "double precision", nullable: false),
                    air_temperature = table.Column<double>(type: "double precision", nullable: false),
                    ph = table.Column<double>(type: "double precision", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_pictures_DOMonitoringGuid",
                table: "pictures",
                column: "DOMonitoringGuid");

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_do_monitoring_id",
                table: "device_infos",
                column: "do_monitoring_id",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_device_infos_do_monitorings_do_monitoring_id",
                table: "device_infos",
                column: "do_monitoring_id",
                principalTable: "do_monitorings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_do_monitorings_DOMonitoringGuid",
                table: "pictures",
                column: "DOMonitoringGuid",
                principalTable: "do_monitorings",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_device_infos_do_monitorings_do_monitoring_id",
                table: "device_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_do_monitorings_DOMonitoringGuid",
                table: "pictures");

            migrationBuilder.DropTable(
                name: "do_monitorings");

            migrationBuilder.DropIndex(
                name: "IX_pictures_DOMonitoringGuid",
                table: "pictures");

            migrationBuilder.DropIndex(
                name: "IX_device_infos_do_monitoring_id",
                table: "device_infos");

            migrationBuilder.DropColumn(
                name: "DOMonitoringGuid",
                table: "pictures");

            migrationBuilder.DropColumn(
                name: "do_monitoring_id",
                table: "device_infos");
        }
    }
}
