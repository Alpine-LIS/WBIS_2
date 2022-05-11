using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class CnddbQuadElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cnddb_quad_elements",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    elm_type = table.Column<string>(type: "text", nullable: true),
                    sci_name = table.Column<string>(type: "text", nullable: true),
                    common_name = table.Column<string>(type: "text", nullable: true),
                    elm_code = table.Column<string>(type: "text", nullable: true),
                    fed_status = table.Column<string>(type: "text", nullable: true),
                    cal_status = table.Column<string>(type: "text", nullable: true),
                    cdfw_status = table.Column<string>(type: "text", nullable: true),
                    rare_plant_rank = table.Column<string>(type: "text", nullable: true),
                    data_status = table.Column<string>(type: "text", nullable: true),
                    taxon_sort = table.Column<string>(type: "text", nullable: true),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_quad_elements", x => x.guid);
                    table.ForeignKey(
                        name: "FK_cnddb_quad_elements_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "cnddb_quad_elements_districts",
                schema: "public",
                columns: table => new
                {
                    cnddb_quad_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_quad_elements_districts", x => new { x.cnddb_quad_element_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_cnddb_quad_elements_districts_cnddb_quad_elements_cnddb_qua~",
                        column: x => x.cnddb_quad_element_id,
                        principalTable: "cnddb_quad_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnddb_quad_elements_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_quad_elements_quad75_id",
                table: "cnddb_quad_elements",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_quad_elements_districts_district_id",
                schema: "public",
                table: "cnddb_quad_elements_districts",
                column: "district_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cnddb_quad_elements_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_quad_elements");
        }
    }
}
