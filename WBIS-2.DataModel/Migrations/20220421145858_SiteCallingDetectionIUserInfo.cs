using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SiteCallingDetectionIUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "record_type",
                table: "site_callings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "site_calling_detections",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_added",
                table: "site_calling_detections",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_modified",
                table: "site_calling_detections",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "site_calling_detections",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "site_calling_detections",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_user_id",
                table: "site_calling_detections",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_user_modified_id",
                table: "site_calling_detections",
                column: "user_modified_id");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_application_users_user_id",
                table: "site_calling_detections",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_calling_detections_application_users_user_modified_id",
                table: "site_calling_detections",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_application_users_user_id",
                table: "site_calling_detections");

            migrationBuilder.DropForeignKey(
                name: "FK_site_calling_detections_application_users_user_modified_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_user_id",
                table: "site_calling_detections");

            migrationBuilder.DropIndex(
                name: "IX_site_calling_detections_user_modified_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "date_added",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "date_modified",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "site_calling_detections");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "site_calling_detections");

            migrationBuilder.AlterColumn<string>(
                name: "record_type",
                table: "site_callings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
