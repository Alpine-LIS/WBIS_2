using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UserFlexRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_flex_records",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    data_form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    repository = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_modified_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_flex_records", x => x.guid);
                    table.ForeignKey(
                        name: "FK_user_flex_records_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_user_flex_records_application_users_user_modified_id",
                        column: x => x.user_modified_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_user_flex_records_data_forms_data_form_id",
                        column: x => x.data_form_id,
                        principalSchema: "flex",
                        principalTable: "data_forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_flex_records_data_form_id",
                table: "user_flex_records",
                column: "data_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_flex_records_user_id",
                table: "user_flex_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_flex_records_user_modified_id",
                table: "user_flex_records",
                column: "user_modified_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_flex_records");
        }
    }
}
