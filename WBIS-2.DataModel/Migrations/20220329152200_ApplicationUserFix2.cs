using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ApplicationUserFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_users_application_users_admin_id",
                table: "application_users");

            migrationBuilder.DropIndex(
                name: "IX_application_users_admin_id",
                table: "application_users");

            migrationBuilder.DropColumn(
                name: "admin_id",
                table: "application_users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "admin_id",
                table: "application_users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_application_users_admin_id",
                table: "application_users",
                column: "admin_id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_users_application_users_admin_id",
                table: "application_users",
                column: "admin_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
