using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixForeignKeysOneToOneTest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_district_extended_geometries_district_id",
                table: "district_extended_geometries");

            migrationBuilder.DropColumn(
                name: "district_extended_geometry_id",
                table: "districts");

            migrationBuilder.CreateIndex(
                name: "IX_district_extended_geometries_district_id",
                table: "district_extended_geometries",
                column: "district_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_district_extended_geometries_district_id",
                table: "district_extended_geometries");

            migrationBuilder.AddColumn<Guid>(
                name: "district_extended_geometry_id",
                table: "districts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_district_extended_geometries_district_id",
                table: "district_extended_geometries",
                column: "district_id",
                unique: true);
        }
    }
}
