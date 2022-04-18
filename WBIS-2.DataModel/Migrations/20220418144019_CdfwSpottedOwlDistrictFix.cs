using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class CdfwSpottedOwlDistrictFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "cdfw_spotted_owl_diagrams",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "cdfw_spotted_owl_diagrams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owl_diagrams_districts_district_id",
                table: "cdfw_spotted_owl_diagrams",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
