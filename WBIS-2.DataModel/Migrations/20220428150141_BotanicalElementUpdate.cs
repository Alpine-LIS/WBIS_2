using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class BotanicalElementUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comments",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "date_time",
                table: "botanical_elements");

            migrationBuilder.AddColumn<string>(
                name: "comments",
                table: "botanical_points_of_interest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "botanical_points_of_interest",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "comments",
                table: "botanical_plants_of_interest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "botanical_plants_of_interest",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "comments",
                table: "botanical_plants_list",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "botanical_plants_list",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comments",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "date_time",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "comments",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "date_time",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "comments",
                table: "botanical_plants_list");

            migrationBuilder.DropColumn(
                name: "date_time",
                table: "botanical_plants_list");

            migrationBuilder.AddColumn<string>(
                name: "comments",
                table: "botanical_elements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "botanical_elements",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
