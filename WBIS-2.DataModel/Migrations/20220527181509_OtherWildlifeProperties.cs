using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class OtherWildlifeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date_time",
                table: "other_wildlife_records",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "detection",
                table: "other_wildlife_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "other_wildlife_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_time",
                table: "other_wildlife_records");

            migrationBuilder.DropColumn(
                name: "detection",
                table: "other_wildlife_records");

            migrationBuilder.DropColumn(
                name: "number",
                table: "other_wildlife_records");
        }
    }
}
