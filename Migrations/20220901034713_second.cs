using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_Manager_Id",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_Manager_Id",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Manager_Id",
                table: "Departments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Manager_Id",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Manager_Id",
                table: "Departments",
                column: "Manager_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_Manager_Id",
                table: "Departments",
                column: "Manager_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
