using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixForeignKeysOneToOneTest4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_elements_amphibian_elem~",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_elements_amphibian_e~",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_list_botanical_elements_botanical_element_~",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_of_interest_botanical_elements_botanical_e~",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_botanical_e~",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_tracks_site_callings_site_calling_id",
                table: "site_calling_tracks");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_tracks_site_calling_id",
                table: "site_calling_tracks");

            migrationBuilder.DropIndex(
                name: "IX_botanical_points_of_interest_botanical_element_id",
                table: "botanical_points_of_interest");

            migrationBuilder.DropIndex(
                name: "IX_botanical_plants_of_interest_botanical_element_id",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropIndex(
                name: "IX_botanical_plants_list_botanical_element_id",
                table: "botanical_plants_list");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_points_of_interest_amphibian_element_id",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_locations_found_amphibian_element_id",
                table: "amphibian_locations_found");

            migrationBuilder.DropColumn(
                name: "device_info_id",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "site_calling_track_id",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "site_calling_id",
                table: "site_calling_tracks");

            migrationBuilder.DropColumn(
                name: "device_info_id",
                table: "owl_bandings");

            migrationBuilder.DropColumn(
                name: "device_info_id",
                table: "botanical_surveys");

            migrationBuilder.DropColumn(
                name: "botanical_element_id",
                table: "botanical_points_of_interest");

            migrationBuilder.DropColumn(
                name: "botanical_element_id",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropColumn(
                name: "botanical_element_id",
                table: "botanical_plants_list");

            migrationBuilder.DropColumn(
                name: "botanical_plant_list_id",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "botanical_plant_of_interest_id",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "botanical_point_of_interest_id",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "device_info_id",
                table: "botanical_elements");

            migrationBuilder.DropColumn(
                name: "device_info_id",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "amphibian_element_id",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropColumn(
                name: "amphibian_element_id",
                table: "amphibian_locations_found");

            migrationBuilder.DropColumn(
                name: "amphibian_location_found_id",
                table: "amphibian_elements");

            migrationBuilder.DropColumn(
                name: "amphibian_point_of_interest_id",
                table: "amphibian_elements");

            migrationBuilder.DropColumn(
                name: "device_info_id",
                table: "amphibian_elements");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest",
                column: "guid",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_of_interest_botanical_elements_guid",
                table: "botanical_plants_of_interest",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_guid",
                table: "botanical_points_of_interest",
                column: "guid",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks",
                column: "guid",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_elements_guid",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_elements_guid",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_list_botanical_elements_guid",
                table: "botanical_plants_list");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_of_interest_botanical_elements_guid",
                table: "botanical_plants_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_guid",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks");

            migrationBuilder.AddColumn<Guid>(
                name: "device_info_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "site_calling_track_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "site_calling_id",
                table: "site_calling_tracks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "device_info_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "device_info_id",
                table: "botanical_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_element_id",
                table: "botanical_points_of_interest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_element_id",
                table: "botanical_plants_of_interest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_element_id",
                table: "botanical_plants_list",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_plant_list_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_plant_of_interest_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "botanical_point_of_interest_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "device_info_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "device_info_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "amphibian_element_id",
                table: "amphibian_points_of_interest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "amphibian_element_id",
                table: "amphibian_locations_found",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "amphibian_location_found_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "amphibian_point_of_interest_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "device_info_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_tracks_site_calling_id",
                table: "site_calling_tracks",
                column: "site_calling_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_points_of_interest_botanical_element_id",
                table: "botanical_points_of_interest",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_of_interest_botanical_element_id",
                table: "botanical_plants_of_interest",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_list_botanical_element_id",
                table: "botanical_plants_list",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_amphibian_element_id",
                table: "amphibian_points_of_interest",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_amphibian_element_id",
                table: "amphibian_locations_found",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_elements_amphibian_elem~",
                table: "amphibian_locations_found",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_elements_amphibian_e~",
                table: "amphibian_points_of_interest",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_list_botanical_elements_botanical_element_~",
                table: "botanical_plants_list",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_of_interest_botanical_elements_botanical_e~",
                table: "botanical_plants_of_interest",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_botanical_e~",
                table: "botanical_points_of_interest",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_tracks_site_callings_site_calling_id",
                table: "site_calling_tracks",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
