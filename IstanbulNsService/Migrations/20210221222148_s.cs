using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Langs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Key = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Langs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPayed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Surname = table.Column<string>(maxLength: 300, nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceNames_Langs_LangId",
                        column: x => x.LangId,
                        principalTable: "Langs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceNames_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<string>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePictures_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    Info = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Location = table.Column<string>(maxLength: 400, nullable: false),
                    WorkTimeFrom = table.Column<DateTime>(nullable: false),
                    WorkTimeTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsInfos_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorsInfos_Langs_LangId",
                        column: x => x.LangId,
                        principalTable: "Langs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineQueries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 256, nullable: false),
                    Code = table.Column<int>(nullable: false),
                    IsSchedule = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineQueries_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneDocs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 200, nullable: false),
                    DoctorsInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneDocs_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneDocs_DoctorsInfos_DoctorsInfoId",
                        column: x => x.DoctorsInfoId,
                        principalTable: "DoctorsInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ServiceId",
                table: "Doctors",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DoctorId",
                table: "DoctorsInfos",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_LangId",
                table: "DoctorsInfos",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineQueries_DoctorId",
                table: "OnlineQueries",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneDocs_DoctorId",
                table: "PhoneDocs",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneDocs_DoctorsInfoId",
                table: "PhoneDocs",
                column: "DoctorsInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceNames_LangId",
                table: "ServiceNames",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceNames_ServiceId",
                table: "ServiceNames",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePictures_ServiceId",
                table: "ServicePictures",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineQueries");

            migrationBuilder.DropTable(
                name: "PhoneDocs");

            migrationBuilder.DropTable(
                name: "ServiceNames");

            migrationBuilder.DropTable(
                name: "ServicePictures");

            migrationBuilder.DropTable(
                name: "DoctorsInfos");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Langs");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
