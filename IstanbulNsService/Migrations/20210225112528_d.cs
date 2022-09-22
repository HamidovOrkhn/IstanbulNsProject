using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsInfos_Langs_LangId",
                table: "DoctorsInfos");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "DoctorsInfos");

            migrationBuilder.DropColumn(
                name: "WorkTimeFrom",
                table: "DoctorsInfos");

            migrationBuilder.DropColumn(
                name: "WorkTimeTo",
                table: "DoctorsInfos");

            migrationBuilder.AlterColumn<int>(
                name: "LangId",
                table: "DoctorsInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WorkTimeFromDate",
                table: "DoctorsInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkTimeFromTime",
                table: "DoctorsInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkTimeToDate",
                table: "DoctorsInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkTimeToTime",
                table: "DoctorsInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InfoStr",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoStr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoStr_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoStr_Langs_LangId",
                        column: x => x.LangId,
                        principalTable: "Langs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoStr_DoctorId",
                table: "InfoStr",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoStr_LangId",
                table: "InfoStr",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsInfos_Langs_LangId",
                table: "DoctorsInfos",
                column: "LangId",
                principalTable: "Langs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsInfos_Langs_LangId",
                table: "DoctorsInfos");

            migrationBuilder.DropTable(
                name: "InfoStr");

            migrationBuilder.DropColumn(
                name: "WorkTimeFromDate",
                table: "DoctorsInfos");

            migrationBuilder.DropColumn(
                name: "WorkTimeFromTime",
                table: "DoctorsInfos");

            migrationBuilder.DropColumn(
                name: "WorkTimeToDate",
                table: "DoctorsInfos");

            migrationBuilder.DropColumn(
                name: "WorkTimeToTime",
                table: "DoctorsInfos");

            migrationBuilder.AlterColumn<int>(
                name: "LangId",
                table: "DoctorsInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "DoctorsInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkTimeFrom",
                table: "DoctorsInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkTimeTo",
                table: "DoctorsInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsInfos_Langs_LangId",
                table: "DoctorsInfos",
                column: "LangId",
                principalTable: "Langs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
