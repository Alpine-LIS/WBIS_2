using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SpiPlantPolyForeignKeyIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_districts_district_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_districts_district_id",
                table: "spi_plant_points",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_districts_district_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points");

            migrationBuilder.DropForeignKey(
                name: "FK_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "spi_plant_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_districts_district_id",
                table: "spi_plant_points",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_hex160s_hex160_id",
                table: "spi_plant_points",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_quad75s_quad75_id",
                table: "spi_plant_points",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spi_plant_points_watersheds_watershed_id",
                table: "spi_plant_points",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
