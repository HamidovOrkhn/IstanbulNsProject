using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class gg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorLevel",
                table: "Doctors",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorLevel",
                table: "Doctors");
        }
    }
}
