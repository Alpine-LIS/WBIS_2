using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixForeignKeysOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_points_of_interest_botanical_p~",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_points_of_interest_amphibian_elements_amphibian_e~",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_district_extended_geometries_districts_guid",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_tracks_site_callings_guid",
                table: "site_calling_tracks");

            migrationBuilder.DropIndex(
                name: "IX_botanical_points_of_interest_amphibian_element_id",
                table: "botanical_points_of_interest");

            migrationBuilder.DropIndex(
                name: "IX_botanical_elements_botanical_point_of_interest_id",
                table: "botanical_elements");

            migrationBuilder.RenameColumn(
                name: "amphibian_element_id",
                table: "botanical_points_of_interest",
                newName: "botanical_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_tracks_site_calling_id",
                table: "site_calling_tracks",
                column: "site_calling_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_district_extended_geometries_district_id",
                table: "district_extended_geometries",
                column: "district_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_points_of_interest_botanical_element_id",
                table: "botanical_points_of_interest",
                column: "botanical_element_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_botanical_e~",
                table: "botanical_points_of_interest",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_district_extended_geometries_districts_district_id",
                table: "district_extended_geometries",
                column: "district_id",
                principalTable: "districts",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_points_of_interest_botanical_elements_botanical_e~",
                table: "botanical_points_of_interest");

            migrationBuilder.DropForeignKey(
                name: "FK_district_extended_geometries_districts_district_id",
                table: "district_extended_geometries");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_tracks_site_callings_site_calling_id",
                table: "site_calling_tracks");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_tracks_site_calling_id",
                table: "site_calling_tracks");

            migrationBuilder.DropIndex(
                name: "IX_district_extended_geometries_district_id",
                table: "district_extended_geometries");

            migrationBuilder.DropIndex(
                name: "IX_botanical_points_of_interest_botanical_element_id",
                table: "botanical_points_of_interest");

            migrationBuilder.RenameColumn(
                name: "botanical_element_id",
                table: "botanical_points_of_interest",
                newName: "amphibian_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_points_of_interest_amphibian_element_id",
                table: "botanical_points_of_interest",
                column: "amphibian_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_elements_botanical_point_of_interest_id",
                table: "botanical_elements",
                column: "botanical_point_of_interest_id");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_points_of_interest_botanical_p~",
                table: "botanical_elements",
                column: "botanical_point_of_interest_id",
                principalTable: "botanical_points_of_interest",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_points_of_interest_amphibian_elements_amphibian_e~",
                table: "botanical_points_of_interest",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_district_extended_geometries_districts_guid",
                table: "district_extended_geometries",
                column: "guid",
                principalTable: "districts",
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
    }
}
