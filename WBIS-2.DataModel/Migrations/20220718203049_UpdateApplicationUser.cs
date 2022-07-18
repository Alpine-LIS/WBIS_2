using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UpdateApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "oid",
                table: "application_users");

            migrationBuilder.DropColumn(
                name: "require_password_change",
                table: "application_users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "oid",
                table: "application_users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "require_password_change",
                table: "application_users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
