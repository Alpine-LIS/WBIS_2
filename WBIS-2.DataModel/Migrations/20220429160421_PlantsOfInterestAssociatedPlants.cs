using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class PlantsOfInterestAssociatedPlants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "botanical_plant_of_interest_id",
                table: "botanical_plants_list",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_botanical_plants_list_botanical_plant_of_interest_id",
                table: "botanical_plants_list",
                column: "botanical_plant_of_interest_id");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_plants_list_botanical_plants_of_interest_botanica~",
                table: "botanical_plants_list",
                column: "botanical_plant_of_interest_id",
                principalTable: "botanical_plants_of_interest",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_plants_list_botanical_plants_of_interest_botanica~",
                table: "botanical_plants_list");

            migrationBuilder.DropIndex(
                name: "IX_botanical_plants_list_botanical_plant_of_interest_id",
                table: "botanical_plants_list");

            migrationBuilder.DropColumn(
                name: "botanical_plant_of_interest_id",
                table: "botanical_plants_list");
        }
    }
}
