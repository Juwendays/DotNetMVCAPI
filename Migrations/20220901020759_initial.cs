using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    MinSalary = table.Column<int>(nullable: false),
                    MaxSalary = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Region_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_Region_Id",
                        column: x => x.Region_Id,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    HireDate = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<int>(nullable: false),
                    Job_Id = table.Column<int>(nullable: false),
                    Manager_Id = table.Column<int>(nullable: false),
                    Department_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_Job_Id",
                        column: x => x.Job_Id,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_Manager_Id",
                        column: x => x.Manager_Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Manager_Id = table.Column<int>(nullable: false),
                    Location_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Locations_Location_Id",
                        column: x => x.Location_Id,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departments_Employees_Manager_Id",
                        column: x => x.Manager_Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Job_Id = table.Column<int>(nullable: false),
                    Department_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobHistories_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobHistories_Employees_Id",
                        column: x => x.Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JobHistories_Jobs_Job_Id",
                        column: x => x.Job_Id,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Region_Id",
                table: "Countries",
                column: "Region_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Location_Id",
                table: "Departments",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Manager_Id",
                table: "Departments",
                column: "Manager_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_Id",
                table: "Employees",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Job_Id",
                table: "Employees",
                column: "Job_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Manager_Id",
                table: "Employees",
                column: "Manager_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistories_Department_Id",
                table: "JobHistories",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistories_Job_Id",
                table: "JobHistories",
                column: "Job_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Country_Id",
                table: "Locations",
                column: "Country_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Department_Id",
                table: "Employees",
                column: "Department_Id",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Regions_Region_Id",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Locations_Location_Id",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_Manager_Id",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "JobHistories");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
