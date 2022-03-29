using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ApplicationUserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_users_application_users_modified_user_id",
                table: "application_users");

            migrationBuilder.RenameColumn(
                name: "modified_user_id",
                table: "application_users",
                newName: "admin_id");

            migrationBuilder.RenameIndex(
                name: "IX_application_users_modified_user_id",
                table: "application_users",
                newName: "IX_application_users_admin_id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_users_application_users_admin_id",
                table: "application_users",
                column: "admin_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_users_application_users_admin_id",
                table: "application_users");

            migrationBuilder.RenameColumn(
                name: "admin_id",
                table: "application_users",
                newName: "modified_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_application_users_admin_id",
                table: "application_users",
                newName: "IX_application_users_modified_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_users_application_users_modified_user_id",
                table: "application_users",
                column: "modified_user_id",
                principalTable: "application_users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
