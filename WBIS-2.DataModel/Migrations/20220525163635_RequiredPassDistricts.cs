using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class RequiredPassDistricts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hex160_required_passes_districts",
                schema: "public",
                columns: table => new
                {
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_required_pass_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160_required_passes_districts", x => new { x.district_id, x.hex160_required_pass_id });
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_districts_hex160_required_passes_hex~",
                        column: x => x.hex160_required_pass_id,
                        principalTable: "hex160_required_passes",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hex160_required_passes_districts_hex160_required_pass_id",
                schema: "public",
                table: "hex160_required_passes_districts",
                column: "hex160_required_pass_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hex160_required_passes_districts",
                schema: "public");
        }
    }
}
