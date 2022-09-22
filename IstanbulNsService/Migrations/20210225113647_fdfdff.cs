using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class fdfdff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WorkTimeToTime",
                table: "DoctorsInfos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "WorkTimeFromTime",
                table: "DoctorsInfos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WorkTimeToTime",
                table: "DoctorsInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "WorkTimeFromTime",
                table: "DoctorsInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
