using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FlexTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nesting_status");

            migrationBuilder.DropTable(
                name: "reproductive_status");

            migrationBuilder.DropTable(
                name: "spow_occupancy_status");

            migrationBuilder.EnsureSchema(
                name: "flex");

            migrationBuilder.CreateTable(
                name: "templates",
                schema: "flex",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    navigation_table_name = table.Column<string>(type: "text", nullable: true),
                    navigation_table_field = table.Column<string>(type: "text", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    report = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "data_forms",
                schema: "flex",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_forms", x => x.id);
                    table.ForeignKey(
                        name: "FK_data_forms_templates_template_id",
                        column: x => x.template_id,
                        principalSchema: "flex",
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_fields",
                schema: "flex",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    field_type = table.Column<string>(type: "text", nullable: false),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    field_order = table.Column<int>(type: "integer", nullable: false),
                    caption = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    font_size = table.Column<int>(type: "integer", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    table_name = table.Column<string>(type: "text", nullable: true),
                    table_field = table.Column<string>(type: "text", nullable: true),
                    custom_script = table.Column<string>(type: "text", nullable: true),
                    image_data = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_fields", x => x.id);
                    table.ForeignKey(
                        name: "FK_template_fields_templates_template_id",
                        column: x => x.template_id,
                        principalSchema: "flex",
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "data_form_fields",
                schema: "flex",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_field_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    string_data = table.Column<string>(type: "text", nullable: true),
                    double_data = table.Column<double>(type: "double precision", nullable: true),
                    boolean_data = table.Column<bool>(type: "boolean", nullable: true),
                    binary_data = table.Column<byte[]>(type: "bytea", nullable: true),
                    geometry = table.Column<Geometry>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_form_fields", x => x.id);
                    table.ForeignKey(
                        name: "FK_data_form_fields_data_forms_data_form_id",
                        column: x => x.data_form_id,
                        principalSchema: "flex",
                        principalTable: "data_forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_form_fields_template_fields_template_field_id",
                        column: x => x.template_field_id,
                        principalSchema: "flex",
                        principalTable: "template_fields",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_data_form_fields_data_form_id",
                schema: "flex",
                table: "data_form_fields",
                column: "data_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_data_form_fields_template_field_id",
                schema: "flex",
                table: "data_form_fields",
                column: "template_field_id");

            migrationBuilder.CreateIndex(
                name: "IX_data_forms_template_id",
                schema: "flex",
                table: "data_forms",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "IX_template_fields_template_id",
                schema: "flex",
                table: "template_fields",
                column: "template_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_form_fields",
                schema: "flex");

            migrationBuilder.DropTable(
                name: "data_forms",
                schema: "flex");

            migrationBuilder.DropTable(
                name: "template_fields",
                schema: "flex");

            migrationBuilder.DropTable(
                name: "templates",
                schema: "flex");

            migrationBuilder.CreateTable(
                name: "nesting_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "reproductive_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "spow_occupancy_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
