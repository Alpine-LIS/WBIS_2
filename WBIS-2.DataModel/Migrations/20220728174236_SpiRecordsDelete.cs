using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SpiRecordsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_added",
                table: "application_users");

            migrationBuilder.DropColumn(
                name: "date_modified",
                table: "application_users");

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "spi_wildlife_sightings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "spi_spows",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "spi_plant_polygons",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "spi_plant_points",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "spi_nogos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "spi_ggows",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_delete",
                table: "spi_wildlife_sightings");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "spi_spows");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "spi_plant_polygons");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "spi_plant_points");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "spi_nogos");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "spi_ggows");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_added",
                table: "application_users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_modified",
                table: "application_users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
