using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class initialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentEmployees",
                columns: table => new
                {
                    DepartmentEmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentEmployees", x => x.DepartmentEmployeeId);
                    table.ForeignKey(
                        name: "FK_DepartmentEmployees_CustomUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CustomUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentAbout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentisDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Firms",
                columns: table => new
                {
                    FirmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmTaxNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmTelNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firms", x => x.FirmId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceAbout = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettingValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserApplicationFirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    DemandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemandTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemandContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemandCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DemandReadTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DemandStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.DemandId);
                    table.ForeignKey(
                        name: "FK_Demands_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demands_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeServices",
                columns: table => new
                {
                    EmployeeServicesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeServices", x => x.EmployeeServicesId);
                    table.ForeignKey(
                        name: "FK_EmployeeServices_CustomUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CustomUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FirmServices",
                columns: table => new
                {
                    FirmServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    FirmServiceCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmServices", x => x.FirmServiceId);
                    table.ForeignKey(
                        name: "FK_FirmServices_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "FirmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirmServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDepartments",
                columns: table => new
                {
                    ServiceDepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDepartments", x => x.ServiceDepartmentId);
                    table.ForeignKey(
                        name: "FK_ServiceDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDepartments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandAnswers",
                columns: table => new
                {
                    DemandAnswersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandId = table.Column<int>(type: "int", nullable: false),
                    DemandAnswerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DemandAnswerType = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandAnswers", x => x.DemandAnswersId);
                    table.ForeignKey(
                        name: "FK_DemandAnswers_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "DemandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandFiles",
                columns: table => new
                {
                    DemandFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandId = table.Column<int>(type: "int", nullable: false),
                    DemandFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandFiles", x => x.DemandFileId);
                    table.ForeignKey(
                        name: "FK_DemandFiles_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "DemandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDemands",
                columns: table => new
                {
                    EmployeeDemandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDemands", x => x.EmployeeDemandId);
                    table.ForeignKey(
                        name: "FK_EmployeeDemands_CustomUser_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "CustomUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDemands_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "DemandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "SettingName", "SettingValue" },
                values: new object[] { 1, "DeleteLogFilterByDay", "2" });

            migrationBuilder.CreateIndex(
                name: "IX_DemandAnswers_DemandId",
                table: "DemandAnswers",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandFiles_DemandId",
                table: "DemandFiles",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_DepartmentId",
                table: "Demands",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_ServiceId",
                table: "Demands",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEmployees_UserId",
                table: "DepartmentEmployees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDemands_DemandId",
                table: "EmployeeDemands",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDemands_EmployeeId",
                table: "EmployeeDemands",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeServices_ServiceId",
                table: "EmployeeServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeServices_UserId",
                table: "EmployeeServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmServices_FirmId",
                table: "FirmServices",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmServices_ServiceId",
                table: "FirmServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDepartments_DepartmentId",
                table: "ServiceDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDepartments_ServiceId",
                table: "ServiceDepartments",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandAnswers");

            migrationBuilder.DropTable(
                name: "DemandFiles");

            migrationBuilder.DropTable(
                name: "DepartmentEmployees");

            migrationBuilder.DropTable(
                name: "EmployeeDemands");

            migrationBuilder.DropTable(
                name: "EmployeeServices");

            migrationBuilder.DropTable(
                name: "FirmServices");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ServiceDepartments");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Demands");

            migrationBuilder.DropTable(
                name: "Firms");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
