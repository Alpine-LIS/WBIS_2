using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixAmphibianSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "water_temp",
             table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "silt",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "sand",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "run",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "riffle",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "pool",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "gravel",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "est_avg_stream_width",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "date_time",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "cobble",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "canopy_closure",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "boulders",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "bedrock",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "air_temp",
                table: "amphibian_surveys");

            migrationBuilder.AddColumn<double>(
                name: "water_temp",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "silt",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "sand",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "run",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "riffle",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "pool",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "gravel",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "est_avg_stream_width",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "amphibian_surveys",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "cobble",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "canopy_closure",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "boulders",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "bedrock",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "air_temp",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "water_temp",
             table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "silt",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "sand",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "run",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "riffle",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "pool",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "gravel",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "est_avg_stream_width",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "date_time",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "cobble",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "canopy_closure",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "boulders",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "bedrock",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "air_temp",
                table: "amphibian_surveys");

            migrationBuilder.AddColumn<double>(
                name: "water_temp",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "silt",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "sand",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "run",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "riffle",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "pool",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "gravel",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "est_avg_stream_width",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "amphibian_surveys",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "cobble",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "canopy_closure",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "boulders",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "bedrock",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "air_temp",
                table: "amphibian_surveys",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
