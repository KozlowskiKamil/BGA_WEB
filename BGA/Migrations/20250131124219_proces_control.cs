using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGA.Migrations
{
    public partial class proces_control : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fail",
                table: "Repair",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pass",
                table: "Repair",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fail",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "Pass",
                table: "Repair");
        }
    }
}
